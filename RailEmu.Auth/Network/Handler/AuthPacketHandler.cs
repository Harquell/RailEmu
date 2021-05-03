using RailEmu.Common.Utils;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.Auth.Network.Handler
{
    public static class AuthPacketHandler
    {
        public static Dictionary<uint, Action<BigEndianReader, AuthClient, AuthServer>> packetList = new Dictionary<uint, Action<BigEndianReader, AuthClient, AuthServer>>
        {
            {4,     ConnectionActions.HandleIdentification },
            {40,    ConnectionActions.HandleServerSelection},
            {6194,  ConnectionActions.HandleIdentificationWithServerId},
            {5639,  ConnectionActions.HandleNicknameChoiceRequest},
            {6144,  ConnectionActions.HandleSearchUserCharacters},
            {76,    ConnectionActions.HandleAuthAdminCommand},
        };

        public static void HandlePacket(byte[] data, AuthClient client, AuthServer server)
        {
            BigEndianReader reader = new BigEndianReader(data);
            AuthClient cl = client;
            short header = reader.ReadShort();
            uint Id = (uint)header >> 2;
            uint Length = (uint)header & 3;
            reader = UpdateReader(reader, Length);
            if (!packetList.ContainsKey(Id))
            {
                Out.Warn($"Unexpected packet from { client.Ip}. Invalid packet ID[{ Id}]" +
                        "\nDisconnecting client");
                client.Disconnect();
                return;
            }
            Out.Debug($"{"New Packet",-12} : {packetList[Id].Method.Name.Remove(0, 6),30}[{Id,4}] <-- {client.Ip,-12}");
            packetList[Id](reader, client, server);
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
