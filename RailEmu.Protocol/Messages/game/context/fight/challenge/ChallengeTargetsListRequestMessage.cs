
















// Generated on 10/13/2017 02:18:46
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class ChallengeTargetsListRequestMessage : Message
{

public const uint Id = 5614;
public override uint MessageId
{
    get { return Id; }
}

public sbyte challengeId;
        

public ChallengeTargetsListRequestMessage()
{
}

public ChallengeTargetsListRequestMessage(sbyte challengeId)
        {
            this.challengeId = challengeId;
        }
        

public override void Serialize(IDataWriter writer)
{

writer.WriteSByte(challengeId);
            

}

public override void Deserialize(IDataReader reader)
{

challengeId = reader.ReadSByte();
            if (challengeId < 0)
                throw new Exception("Forbidden value on challengeId = " + challengeId + ", it doesn't respect the following condition : challengeId < 0");
            

}


}


}
