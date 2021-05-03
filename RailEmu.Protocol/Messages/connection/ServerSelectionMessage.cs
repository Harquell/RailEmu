
















// Generated on 10/13/2017 02:18:37
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class ServerSelectionMessage : Message
{

public const uint Id = 40;
public override uint MessageId
{
    get { return Id; }
}

public short serverId;
        

public ServerSelectionMessage()
{
}

public ServerSelectionMessage(short serverId)
        {
            this.serverId = serverId;
        }
        

public override void Serialize(IDataWriter writer)
{

writer.WriteShort(serverId);
            

}

public override void Deserialize(IDataReader reader)
{

serverId = reader.ReadShort();
            

}


}


}
