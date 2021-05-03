
















// Generated on 10/13/2017 02:19:07
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Types;

namespace RailEmu.Protocol.Messages
{

public class StartupActionFinishedMessage : Message
{

public const uint Id = 1304;
public override uint MessageId
{
    get { return Id; }
}

public bool success;
        public bool automaticAction;
        public int actionId;
        

public StartupActionFinishedMessage()
{
}

public StartupActionFinishedMessage(bool success, bool automaticAction, int actionId)
        {
            this.success = success;
            this.automaticAction = automaticAction;
            this.actionId = actionId;
        }
        

public override void Serialize(IDataWriter writer)
{

byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, success);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, automaticAction);
            writer.WriteByte(flag1);
            writer.WriteInt(actionId);
            

}

public override void Deserialize(IDataReader reader)
{

byte flag1 = reader.ReadByte();
            success = BooleanByteWrapper.GetFlag(flag1, 0);
            automaticAction = BooleanByteWrapper.GetFlag(flag1, 1);
            actionId = reader.ReadInt();
            if (actionId < 0)
                throw new Exception("Forbidden value on actionId = " + actionId + ", it doesn't respect the following condition : actionId < 0");
            

}


}


}