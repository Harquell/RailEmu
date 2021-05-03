using RailEmu.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.Common.Types
{
    public class ServerData
    {
        public ushort id { get; private set; }

        //CONNECTION INFOS
        public string ip { get; private set; }
        public int port { get; private set; }
        public bool canCreateCharacter { get; private set; }

        //LIST INFOS
        public string name { get; private set; }
        public sbyte status { get; private set; }
        public sbyte completion { get; private set; }
        public bool isSelectable { get; private set; }


        public ServerData(ushort id, string ip, int port, bool canCreateCharacter, string name, sbyte status, sbyte completion, bool isSelectable)
        {
            this.id = id;
            this.ip = ip;
            this.port = port;
            this.canCreateCharacter = canCreateCharacter;
            this.name = name;
            this.status = status;
            this.completion = completion;
            this.isSelectable = isSelectable;
        }

        public ServerData(ushort id, string ip, int port, string name)
        {
            this.id = id;
            this.ip = ip;
            this.port = port;
            this.canCreateCharacter = true;
            this.name = name;
            this.status = (sbyte)ServerStatusEnum.OFFLINE;
            this.completion = 0;
            this.isSelectable = true;
        }
    }
}
