using RailEmu.Common.Database.Manager;
using RailEmu.Common.Database.Modeles.Auth;
using RailEmu.Common.Utils;
using RailEmu.Protocol.Enums;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Messages;
using RailEmu.Protocol.Types;
using System;
using System.Collections.Generic;

namespace RailEmu.Auth.Network.Handler
{
    public static class ConnectionHandler
    {

        public static void BuildPacket(byte[] data, AuthClient client, AuthServer server)
        {
            BigEndianReader reader = new BigEndianReader(data);
            AuthClient cl = client;
            short header = reader.ReadShort();
            uint Id = (uint)header >> 2;
            uint Length = (uint)header & 3;
            reader = UpdateReader(reader, Length);
            Out.Debug($"=> Packet[{Id}] from {client.Ip}");
            #region
            switch (Id)
            {
                case 4:
                    IdentificationWithLoginTokenMessage message = new IdentificationWithLoginTokenMessage();
                    message.Unpack(reader);
                    Account account = AccountManager.GetAccount(message.login);
                    DateTime dateNow = DateTime.Now;
                    List<GameServerInformations> servers = new List<GameServerInformations>();
                    foreach (WorldServer s in server.Servers.Values) //Load each server
                    {
                        int nbChar = AccountManager.GetNbChar(s.ServerId, account.Id);
                        servers.Add(new GameServerInformations((ushort)s.ServerId, (sbyte)s.Status, (sbyte)s.Completion, true, (sbyte)nbChar));
                    }
                    if (servers.Count < 1) AuthServer.onMaintenance = true;
                    if (account == null ||
                        message.password != Tools.GetMd5(account.Password + cl.Ticket)) //Test password
                    {
                        client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.WRONG_CREDENTIALS));
                        client.Disconnect();
                        return;
                    }
                    if (account.Banned) // ban a vie
                    {
                        client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.BANNED));
                        client.Disconnect();
                        return;
                    }
                    if (account.EndBan > dateNow)//Ban temp
                    {
                        int ms = 0;
                        ms = (int)account.EndBan.Subtract(dateNow).TotalMinutes;
                        client.Send(new IdentificationFailedBannedMessage((sbyte)IdentificationFailureReasonEnum.BANNED, ms));
                        client.Disconnect();
                        return;
                    }
                    if (AuthServer.onMaintenance) //Maintenance (In config)
                    {
                        client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.IN_MAINTENANCE));
                        client.Disconnect();
                        return;
                    }
                    client.account = account;

                    double msSub = 0;
                    if (account.EndSub > DateTime.Now)
                        msSub = account.EndSub.Subtract(DateTime.Now).TotalMilliseconds;
                    client.Send(new IdentificationSuccessMessage(account.isAdmin, true, account.Pseudo, account.Id, 0, account.Question, msSub));
                    Out.Debug(account.isAdmin? $"+Admin {account.Pseudo}" : $"+User {account.Pseudo}");
                    client.Send(new ServersListMessage(servers));

                    //TODO Get number of characters for each server
                    return;
                default:
                    client.Disconnect();
                    return;
            }
            #endregion
        }

        public static BigEndianReader UpdateReader(BigEndianReader reader, uint Length)
        {
            #region switch Length
            switch (Length)
            {
                case 0:
                    Length = 0;
                    break;
                case 1:
                    Length = reader.ReadByte();
                    break;
                case 2:
                    Length = reader.ReadUShort();
                    break;
                case 3:
                    Length = (uint)(((reader.ReadByte() & 255) << 16) + ((reader.ReadByte() & 255) << 8) + (reader.ReadByte() & 255));
                    break;
                default:
                    Length = 0;
                    break;
            }
            return reader;
            #endregion
        }
    }
}
