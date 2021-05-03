
















// Generated on 10/13/2017 02:19:00
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class ExchangeMountPaddockRemoveMessage : Message
{

public const uint Id = 6050;
public override uint MessageId
{
    get { return Id; }
}

public double mountId;
        

public ExchangeMountPaddockRemoveMessage()
{
}

public ExchangeMountPaddockRemoveMessage(double mountId)
        {
            this.mountId = mountId;
        }
        

public override void Serialize(IDataWriter writer)
{

writer.WriteDouble(mountId);
            

}

public override void Deserialize(IDataReader reader)
{

mountId = reader.ReadDouble();
            

}


}


}
