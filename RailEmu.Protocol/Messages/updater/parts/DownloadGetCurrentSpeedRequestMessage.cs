
















// Generated on 10/13/2017 02:19:08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class DownloadGetCurrentSpeedRequestMessage : Message
{

public const uint Id = 1510;
public override uint MessageId
{
    get { return Id; }
}



public DownloadGetCurrentSpeedRequestMessage()
{
}



public override void Serialize(IDataWriter writer)
{



}

public override void Deserialize(IDataReader reader)
{



}


}


}
