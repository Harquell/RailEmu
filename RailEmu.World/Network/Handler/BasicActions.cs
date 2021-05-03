using RailEmu.Common.Database.Manager;
using RailEmu.Common.Database.Modeles.Auth;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.World.Network.Handler
{
    public static class BasicActions
    {
        public static void HandleAuthenticationTicket(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            AuthenticationTicketMessage message = new AuthenticationTicketMessage();
            message.Unpack(reader);
            Account account = AccountManager.GetAccountByTicket(message.ticket);
            if (account == null)
                client.Send(new AuthenticationTicketRefusedMessage());
            client.Ticket = message.ticket;
            client.account = account;
            client.Send(new AuthenticationTicketAcceptedMessage());
        }
    }
}
