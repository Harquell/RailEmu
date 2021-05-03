
















// Generated on 10/13/2017 02:18:42
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class BasicWhoIsMessage : Message
{

public const uint Id = 180;
public override uint MessageId
{
    get { return Id; }
}

public bool self;
        public sbyte position;
        public string accountNickname;
        public string characterName;
        public short areaId;
        

public BasicWhoIsMessage()
{
}

public BasicWhoIsMessage(bool self, sbyte position, string accountNickname, string characterName, short areaId)
        {
            this.self = self;
            this.position = position;
            this.accountNickname = accountNickname;
            this.characterName = characterName;
            this.areaId = areaId;
        }
        

public override void Serialize(IDataWriter writer)
{

writer.WriteBoolean(self);
            writer.WriteSByte(position);
            writer.WriteUTF(accountNickname);
            writer.WriteUTF(characterName);
            writer.WriteShort(areaId);
            

}

public override void Deserialize(IDataReader reader)
{

self = reader.ReadBoolean();
            position = reader.ReadSByte();
            accountNickname = reader.ReadUTF();
            characterName = reader.ReadUTF();
            areaId = reader.ReadShort();
            

}


}


}
