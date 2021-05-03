
















// Generated on 10/13/2017 02:18:42
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class CharacterSelectedSuccessMessage : Message
{

public const uint Id = 153;
public override uint MessageId
{
    get { return Id; }
}

public Types.CharacterBaseInformations infos;
        

public CharacterSelectedSuccessMessage()
{
}

public CharacterSelectedSuccessMessage(Types.CharacterBaseInformations infos)
        {
            this.infos = infos;
        }
        

public override void Serialize(IDataWriter writer)
{

infos.Serialize(writer);
            

}

public override void Deserialize(IDataReader reader)
{

infos = new Types.CharacterBaseInformations();
            infos.Deserialize(reader);
            

}


}


}
