
















// Generated on 10/13/2017 02:18:53
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class PartyNewGuestMessage : Message
{

public const uint Id = 6260;
public override uint MessageId
{
    get { return Id; }
}

public Types.PartyGuestInformations guest;
        

public PartyNewGuestMessage()
{
}

public PartyNewGuestMessage(Types.PartyGuestInformations guest)
        {
            this.guest = guest;
        }
        

public override void Serialize(IDataWriter writer)
{

guest.Serialize(writer);
            

}

public override void Deserialize(IDataReader reader)
{

guest = new Types.PartyGuestInformations();
            guest.Deserialize(reader);
            

}


}


}
