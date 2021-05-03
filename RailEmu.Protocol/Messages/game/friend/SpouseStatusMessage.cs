
















// Generated on 10/13/2017 02:18:56
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class SpouseStatusMessage : Message
{

public const uint Id = 6265;
public override uint MessageId
{
    get { return Id; }
}

public bool hasSpouse;
        

public SpouseStatusMessage()
{
}

public SpouseStatusMessage(bool hasSpouse)
        {
            this.hasSpouse = hasSpouse;
        }
        

public override void Serialize(IDataWriter writer)
{

writer.WriteBoolean(hasSpouse);
            

}

public override void Deserialize(IDataReader reader)
{

hasSpouse = reader.ReadBoolean();
            

}


}


}
