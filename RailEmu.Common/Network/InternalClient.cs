using RailEmu.Common.Utils;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.Common.Network
{
    public abstract class InternalClient
    {
        public Socket socket { get; private set; }
        private byte[] _buffer;
        public event EventHandler<byte[]> onReceivePacket;
        public event EventHandler<Message> onSendPacket;

        public InternalClient(Socket s)
        {
            socket = s;
            _buffer = new byte[8192];
        }

        private void ReceiveCallBack(IAsyncResult result)
        {
            try
            {
                int size = socket.EndReceive(result);
                byte[] buffer = new byte[size];
                Array.Copy(_buffer, buffer, size);

                NewPacket(buffer);
            }
            catch(Exception e)
            {
                Out.Error($"InterServer\t>Error while handling a packet : {e.Message}");
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
                }
            }
            catch (Exception e)
            {
                Out.Warn($"InterServer\t>Error while sending a packet : {e.Message}");
            }
        }


        private void SendCallBack(IAsyncResult result)
        {
            socket = (Socket)result.AsyncState;
            socket.EndSend(result);
        }

        protected virtual void NewPacket(byte[] packet)
            => onReceivePacket?.Invoke(this ,packet);


        
    }
}
