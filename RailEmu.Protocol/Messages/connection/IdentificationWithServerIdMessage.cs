
















// Generated on 10/13/2017 02:18:37
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class IdentificationWithServerIdMessage : IdentificationMessage
{

public const uint Id = 6194;
public override uint MessageId
{
    get { return Id; }
}

public short serverId;
        

public IdentificationWithServerIdMessage()
{
}

public IdentificationWithServerIdMessage(Types.Version version, string login, string password, IEnumerable<Types.TrustCertificate> certificate, bool autoconnect, short serverId)
         : base(version, login, password, certificate, autoconnect)
        {
            this.serverId = serverId;
        }
        

public override void Serialize(IDataWriter writer)
{

base.Serialize(writer);
            writer.WriteShort(serverId);
            

}

public override void Deserialize(IDataReader reader)
{

base.Deserialize(reader);
            serverId = reader.ReadShort();
            

}


}


}
