
















// Generated on 10/13/2017 02:18:46
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class ChallengeTargetUpdateMessage : Message
{

public const uint Id = 6123;
public override uint MessageId
{
    get { return Id; }
}

public sbyte challengeId;
        public int targetId;
        

public ChallengeTargetUpdateMessage()
{
}

public ChallengeTargetUpdateMessage(sbyte challengeId, int targetId)
        {
            this.challengeId = challengeId;
            this.targetId = targetId;
        }
        

public override void Serialize(IDataWriter writer)
{

writer.WriteSByte(challengeId);
            writer.WriteInt(targetId);
            

}

public override void Deserialize(IDataReader reader)
{

challengeId = reader.ReadSByte();
            if (challengeId < 0)
                throw new Exception("Forbidden value on challengeId = " + challengeId + ", it doesn't respect the following condition : challengeId < 0");
            targetId = reader.ReadInt();
            

}


}


}
