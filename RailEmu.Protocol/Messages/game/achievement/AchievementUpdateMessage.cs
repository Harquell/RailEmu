
















// Generated on 10/13/2017 02:18:38
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class AchievementUpdateMessage : Message
{

public const uint Id = 6207;
public override uint MessageId
{
    get { return Id; }
}

public Types.Achievement achievement;
        

public AchievementUpdateMessage()
{
}

public AchievementUpdateMessage(Types.Achievement achievement)
        {
            this.achievement = achievement;
        }
        

public override void Serialize(IDataWriter writer)
{

achievement.Serialize(writer);
            

}

public override void Deserialize(IDataReader reader)
{

achievement = new Types.Achievement();
            achievement.Deserialize(reader);
            

}


}


}
