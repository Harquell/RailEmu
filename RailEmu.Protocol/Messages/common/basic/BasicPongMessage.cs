
















// Generated on 10/13/2017 02:18:37
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class BasicPongMessage : Message
{

public const uint Id = 183;
public override uint MessageId
{
    get { return Id; }
}

public bool quiet;
        

public BasicPongMessage()
{
}

public BasicPongMessage(bool quiet)
        {
            this.quiet = quiet;
        }
        

public override void Serialize(IDataWriter writer)
{

writer.WriteBoolean(quiet);
            

}

public override void Deserialize(IDataReader reader)
{

quiet = reader.ReadBoolean();
            

}


}


}
