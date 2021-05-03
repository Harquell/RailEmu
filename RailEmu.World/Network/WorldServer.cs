using RailEmu.Common.Utils;
using RailEmu.Protocol.Enums;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Messages;
using RailEmu.Protocol.Types;
using RailEmu.World.Network.Handler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RailEmu.World.Network
{
    public class WorldServer
    {
        public Socket _world { get; private set; }

        public GameServerInformations ServerInfos { get; private set; }

        public List<WorldClient> Clients { get; private set; }
        public Dictionary<string, int> PendingTickets { get; private set; }
        public byte[] buffer { get; private set; } = new byte[8192];


        public WorldServer()
        {
            Clients = new List<WorldClient>();
            PendingTickets = new Dictionary<string, int>();
            _world = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _world.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 442));
            _world.Listen(5);

            _world.BeginAccept(AcceptCallBack, _world);

            Out.Info("World server [1] started");
        }

        public void AcceptCallBack(IAsyncResult result)
        {
            try
            {
                WorldClient _newClient = new WorldClient(_world.EndAccept(result), this);
                Out.Debug($"New socket [{_newClient.socket.RemoteEndPoint}]");
                Clients.Add(_newClient);
            }
            catch (Exception e)
            {
                if (Clients != null)
                    Out.Error(e.Message);
            }
            _world.BeginAccept(AcceptCallBack, null);
        }
    }
}
