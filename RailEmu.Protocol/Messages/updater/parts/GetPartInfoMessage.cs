
















// Generated on 10/13/2017 02:19:08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class GetPartInfoMessage : Message
{

public const uint Id = 1506;
public override uint MessageId
{
    get { return Id; }
}

public string id;
        

public GetPartInfoMessage()
{
}

public GetPartInfoMessage(string id)
        {
            this.id = id;
        }
        

public override void Serialize(IDataWriter writer)
{

writer.WriteUTF(id);
            

}

public override void Deserialize(IDataReader reader)
{

id = reader.ReadUTF();
            

}


}


}
