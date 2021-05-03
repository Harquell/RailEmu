
















// Generated on 10/13/2017 02:19:04
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class InventoryPresetUpdateMessage : Message
{

public const uint Id = 6171;
public override uint MessageId
{
    get { return Id; }
}

public Types.Preset preset;
        

public InventoryPresetUpdateMessage()
{
}

public InventoryPresetUpdateMessage(Types.Preset preset)
        {
            this.preset = preset;
        }
        

public override void Serialize(IDataWriter writer)
{

preset.Serialize(writer);
            

}

public override void Deserialize(IDataReader reader)
{

preset = new Types.Preset();
            preset.Deserialize(reader);
            

}


}


}
