using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.Common.Network
{
    public interface IBaseClient
    {
        void Disconnect();
        string Ip { get; }
        int Port { get; }
        Socket socket { get; }
    }
}
