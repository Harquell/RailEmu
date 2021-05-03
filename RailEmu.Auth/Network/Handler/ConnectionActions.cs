using RailEmu.Common.Database;
using RailEmu.Common.Database.Manager;
using RailEmu.Common.Database.Modeles.Auth;
using RailEmu.Common.Utils;
using RailEmu.Protocol.Enums;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Messages;
using RailEmu.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RailEmu.Auth.Network.Handler
{
    static class ConnectionActions
    {
        public static string[] invalidNickname = new string[]
        {
            "ADMIN", "[GM]", "[MJ]", "[BETA]", "{", "}", "[", "]", "\"", "'", "!", "?"
            //TODO Load invalid strings from a XML/JSON/MySQL? base
        };

        public static void HandleIdentification(BigEndianReader reader, AuthClient client, AuthServer server)
        {
            IdentificationWithLoginTokenMessage message = new IdentificationWithLoginTokenMessage();
            message.Unpack(reader);
            Account account = AccountManager.GetAccount(message.login);
            DateTime dateNow = DateTime.Now;
            //IF ACCOUNT EXIST
            if (account == null ||
                message.password != Tools.GetMd5(account.Password + client.Ticket)) //Test password
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.WRONG_CREDENTIALS));
                client.Disconnect();
                return;
            }
            client.account = account;
            //IF LIFE BANNED OR IP BANNED
            if (account.Banned || server.BlockedIp.Contains(client.Ip))
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.BANNED));
                client.Disconnect();
                return;
            }
            //IF TEMP BANNED
            if (account.EndBan > dateNow)
            {
                int ms = 0;
                ms = (int)account.EndBan.Subtract(dateNow).TotalMinutes;
                client.Send(new IdentificationFailedBannedMessage((sbyte)IdentificationFailureReasonEnum.BANNED, ms));
                client.Disconnect();
                return;
            }
            //IF ON MAINTENANCE
            if (AuthServer.onMaintenance && !account.isAdmin)
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.IN_MAINTENANCE));
                client.Disconnect();
                return;
            }
            //TODO QUEUE MANAGEMENT
            if(account.Pseudo == null || account.Pseudo.Length == 0)
            {
                client.Send(new NicknameRegistrationMessage());
                Out.Info($"First connection for account '{account.Username}'. Requesting a nickname.");
                return;
            }
            SendAccServer(client, server);
            //TODO AUTO CONNECT TO THE FIRST AVAILABLE SERVER
            if (message.autoconnect)
                //client.Send(new SelectedServerRefusedMessage(1, (sbyte)ServerConnectionErrorEnum.SERVER_CONNECTION_ERROR_DUE_TO_STATUS, (sbyte)ServerStatusEnum.NOJOIN));
            Out.Debug(account.isAdmin ? $"+Admin {account.Pseudo}" : $"+User {account.Pseudo}");
        }

        public static void HandleServerSelection(BigEndianReader reader, AuthClient client, AuthServer server)
        {
            ServerSelectionMessage message = new ServerSelectionMessage();
            message.Unpack(reader);
            client.Send(new SelectedServerDataMessage(1, "127.0.0.1", 442, true, client.Ticket));
            client.Disconnect();
            //client.Send(new SelectedServerRefusedMessage(message.serverId, (sbyte)ServerConnectionErrorEnum.SERVER_CONNECTION_ERROR_NO_REASON, (sbyte)ServerStatusEnum.NOJOIN));
            //TODO HANDLE SERVER CONNECTION REQUEST FROM CLIENT
        }

        public static void HandleIdentificationWithServerId(BigEndianReader reader, AuthClient client, AuthServer server)
        {
            //TODO RECONNECT TO LAST CONNECTED SERVER BY ID SENT BY THE CLIENT
        }

        public static void HandleNicknameChoiceRequest(BigEndianReader reader, AuthClient client, AuthServer server)
        {
            NicknameChoiceRequestMessage message = new NicknameChoiceRequestMessage();
            message.Unpack(reader);
            List<Account> accounts = AccountManager.GetAccounts();

            //TEST IF NICK == USERNAME
            if(message.nickname == client.account.Username)
            {
                client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.SAME_AS_LOGIN));
                return;
            }

            if (message.nickname.ToLower() == client.account.Username.ToLower() || message.nickname.ToLower().Contains(client.account.Username.ToLower()))
            {
                client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.TOO_SIMILAR_TO_LOGIN));
                return;
            }

            //TEST IF NAME ALREADY TAKEN
            foreach (Account a in accounts)
            {
                if (a.Pseudo == message.nickname)
                {
                    client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.ALREADY_USED));
                    return;
                }
            }

            //TEST IF NAME IS ALLOWED
            foreach(string s in invalidNickname)
            {
                if(message.nickname.Contains(s.ToLower()) || message.nickname.Contains(s.ToUpper()))
                {
                    client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.INVALID_NICK));
                    return;
                }
            }

            //VALID USERNAME
            if(!AccountManager.SetAccountNickname(client.account.Id, message.nickname))
            {
                client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.UNKNOWN_NICK_ERROR));
                return;
            }
            client.Send(new NicknameAcceptedMessage());
            SendAccServer(client, server);
            Out.Info($"Welcome to a new player ! Be kind to {message.nickname} =D", ConsoleColor.Cyan);
        }

        public static void HandleSearchUserCharacters(BigEndianReader reader, AuthClient client, AuthServer server)
        {
            AcquaintanceSearchMessage message = new AcquaintanceSearchMessage();
            message.Unpack(reader);

            foreach(Account a in AccountManager.GetAccounts())
            {
                if(a.Pseudo == message.nickname)
                {
                    List<short> servers = new List<short>();
                    foreach (WorldServer s in server.Servers.Values)
                    {
                        int nbChar = AccountManager.GetNbChar(s.ServerId, a.Id);
                        if (nbChar > 0)
                            servers.Add((short)s.ServerId);
                    }
                    client.Send(new AcquaintanceServerListMessage(servers));
                    return;
                }
            }
            client.Send(new AcquaintanceSearchErrorMessage(2));
        }

        public static void HandleAuthAdminCommand(BigEndianReader reader, AuthClient client, AuthServer server)
        {
            //TODO HANDLE AUTH COMMANDS
            AdminCommandMessage message = new AdminCommandMessage();
            message.Unpack(reader);

            switch (message.content.Split(' ')[0])
            {
                default:
                    client.Send(new ConsoleMessage((sbyte)ConsoleMessageTypeEnum.CONSOLE_ERR_MESSAGE, "Unknown command"));
                    break;
            }

            Out.Debug($"User {client.account.Pseudo} performed command '{message.content}'");
        }

        private static void SendAccServer(AuthClient client, AuthServer server)
        {
            List<GameServerInformations> servers = new List<GameServerInformations>();
            foreach (WorldServer s in server.Servers.Values) //Load each server
            {
                int nbChar = AccountManager.GetNbChar(s.ServerId, client.account.Id);
                servers.Add(new GameServerInformations((ushort)s.ServerId, (sbyte)s.Status, (sbyte)s.Completion, true, (sbyte)nbChar));
            }
            if (servers.Count < 1)
                AuthServer.onMaintenance = true;

            client.account = AccountManager.GetAccount(client.account.Id);
            double msSub = 0;
            if (client.account.EndSub > DateTime.Now)
                msSub = client.account.EndSub.Subtract(DateTime.Now).TotalMilliseconds;
            client.Send(new IdentificationSuccessMessage(client.account.isAdmin, true, client.account.Pseudo, client.account.Id, 0, client.account.Question, msSub));
            client.Send(new ServersListMessage(servers));
        }
    }
}
