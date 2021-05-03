
















// Generated on 10/13/2017 02:18:49
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class GameRolePlayRemoveChallengeMessage : Message
{

public const uint Id = 300;
public override uint MessageId
{
    get { return Id; }
}

public int fightId;
        

public GameRolePlayRemoveChallengeMessage()
{
}

public GameRolePlayRemoveChallengeMessage(int fightId)
        {
            this.fightId = fightId;
        }
        

public override void Serialize(IDataWriter writer)
{

writer.WriteInt(fightId);
            

}

public override void Deserialize(IDataReader reader)
{

fightId = reader.ReadInt();
            

}


}


}
