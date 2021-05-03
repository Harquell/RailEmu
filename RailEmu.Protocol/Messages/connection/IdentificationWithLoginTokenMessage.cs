
















// Generated on 10/13/2017 02:18:37
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class IdentificationWithLoginTokenMessage : IdentificationMessage
{

public const uint Id = 6201;
public override uint MessageId
{
    get { return Id; }
}

public string loginToken;
        

public IdentificationWithLoginTokenMessage()
{
}

public IdentificationWithLoginTokenMessage(Types.Version version, string login, string password, IEnumerable<Types.TrustCertificate> certificate, bool autoconnect, string loginToken)
         : base(version, login, password, certificate, autoconnect)
        {
            this.loginToken = loginToken;
        }
        

public override void Serialize(IDataWriter writer)
{

base.Serialize(writer);
            writer.WriteUTF(loginToken);
            

}

public override void Deserialize(IDataReader reader)
{

base.Deserialize(reader);
            loginToken = reader.ReadUTF();
            

}


}


}
