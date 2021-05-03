
















// Generated on 10/13/2017 02:17:24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;

namespace RailEmu.Protocol.Types
{

public class DungeonPartyFinderPlayer
{

public const short Id = 373;
public virtual short TypeId
{
    get { return Id; }
}

public int playerId;
        public string playerName;
        public sbyte breed;
        public bool sex;
        public short level;
        

public DungeonPartyFinderPlayer()
{
}

public DungeonPartyFinderPlayer(int playerId, string playerName, sbyte breed, bool sex, short level)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.breed = breed;
            this.sex = sex;
            this.level = level;
        }
        

public virtual void Serialize(IDataWriter writer)
{

writer.WriteInt(playerId);
            writer.WriteUTF(playerName);
            writer.WriteSByte(breed);
            writer.WriteBoolean(sex);
            writer.WriteShort(level);
            

}

public virtual void Deserialize(IDataReader reader)
{

playerId = reader.ReadInt();
            if (playerId < 0)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
            playerName = reader.ReadUTF();
            breed = reader.ReadSByte();
            if (breed < (byte)Enums.PlayableBreedEnum.Feca || breed > (byte)Enums.PlayableBreedEnum.Zobal)
                throw new Exception("Forbidden value on breed = " + breed + ", it doesn't respect the following condition : breed < (byte)Enums.PlayableBreedEnum.Feca || breed > (byte)Enums.PlayableBreedEnum.Zobal");
            sex = reader.ReadBoolean();
            level = reader.ReadShort();
            if (level < 0)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 0");
            

}



}


}