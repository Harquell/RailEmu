using RailEmu.Common.Network;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using RailEmu.Common.Database.Modeles.Auth;
using RailEmu.Common.Database;
using RailEmu.Protocol.Messages;
using RailEmu.Protocol.IO;
using RailEmu.Common.Utils;
using System.Net;
using RailEmu.Protocol.Enums;
using RailEmu.Auth.Network.Handler;
using System.Threading;

namespace RailEmu.Auth.Network
{
    public class AuthClient : IBaseClient
    {
        public string Ip { get; private set; }
        public int Port { get; private set; }
        public Socket socket { get; private set; }
        private Thread thread;

        private AuthServer server;
        public Account account;
        public string Ticket;
        private byte[] _buffer = new byte[8192];
        

        public void Initialize()
        {
            try
            {
                Send(new ProtocolRequired(AuthServer.ProtocolVersion, AuthServer.ProtocolVersion));
                Send(new HelloConnectMessage(1, Ticket)); //Send password encryption key
                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallBack, socket);
            }
            catch (Exception)
            {
                Out.Debug($"User {Ip} disconnected because of an error");
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
                AuthPacketHandler.HandlePacket(buffer, this, server);

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
                    Out.Debug($"{"New Packet", -12} : {message.ToString(), 36} --> {Ip, -12}");
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
                return;
            }
        }

        public AuthClient(Socket client, AuthServer auth)
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
            server.Clients.Remove(socket);
            socket.Dispose();
            Out.Debug($" -User {Ip}");
        }
    }
}
