using RailEmu.Common.Database.Modeles.Auth;
using RailEmu.Common.Utils;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Messages;
using RailEmu.World.Network.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RailEmu.World.Network
{
    public class WorldClient
    {
        public string Ip { get; private set; }
        public int Port { get; private set; }
        public Socket socket { get; private set; }
        private Thread thread;

        private WorldServer server;
        public Account account;
        public string Ticket;
        private byte[] _buffer = new byte[8192];


        public void Initialize()
        {
            try
            {
                //TODO HANDLE WORLD CONNECTION
                Send(new ProtocolRequired(1375, 1375));
                Send(new HelloGameMessage());
                Send(new BasicTimeMessage(1, 0));
                Send(new AccountCapabilitiesMessage(1, false, 16383, 4095)); //-> Binary (each bit correspond to one class)
                Send(new TrustStatusMessage(true));
                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallBack, socket);
            }
            catch (Exception e)
            {
                Out.Debug($"User {Ip} disconnected because of an error");
                Out.Error(e.Message);
                Disconnect();
            }
        }

        public void ReceiveCallBack(IAsyncResult result)
        {
            try
            {
                socket = (Socket)result.AsyncState;

                if (!socket.Connected)
                {
                    Disconnect();
                    return;
                }

                int size = socket.EndReceive(result);
                byte[] buffer = new byte[size];
                Array.Copy(_buffer, buffer, buffer.Length);
                BigEndianReader reader = new BigEndianReader(buffer);

                //ConnectionHandler.BuildPacket(buffer, this, server);
                WorldPacketHandler.HandlePacket(buffer, this, server);

                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
            }
            catch (Exception e)
            {
                Disconnect();
                return;
            }
        }
        public void Send(Message message)
        {
            try
            {
                using (BigEndianWriter writer = new BigEndianWriter())
                {
                    message.Pack(writer);
                    socket.BeginSend(writer.Data, 0, writer.Data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), socket);
                    Out.Debug($"{"New Packet",-12} : {message.ToString(),36} --> {Ip,-12}", "World");
                }
            }
            catch (Exception e)
            {
                Out.Warn($" USER DISCONNECTED (sending packet) {e.Message}");
                Disconnect();
            }
        }
        private void SendCallBack(IAsyncResult result)
        {
            try
            {
                socket = (Socket)result.AsyncState;
                socket.EndSend(result);
            }
            catch (Exception e)
            {
                Out.Warn(e.Message);
                return;
            }
        }

        public WorldClient(Socket client, WorldServer auth)
        {
            socket = client;
            server = auth;
            Ip = ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
            Port = ((IPEndPoint)socket.RemoteEndPoint).Port;
            thread = new Thread(Initialize);
            thread.Start();
        }

        public void Disconnect()
        {
            thread.Abort();
            server.Clients.Remove(this);
            socket.Dispose();
            Out.Debug($" -User {Ip}");
        }
    }
}
