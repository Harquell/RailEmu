using RailEmu.Common.Utils;
using RailEmu.Protocol.Enums;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Messages;
using RailEmu.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.World.Network.Handler
{
    public static class TestActions
    {
        //5623 QUEST LIST
        public static void HandleQuestList(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            client.Send(new QuestListMessage(new short[0], new short[0]));
        }


        //4001 getFriendsList
        public static void HandleFriendsList(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            client.Send(new FriendsListMessage(new FriendInformations[0]));
        }

        //5676
        public static void HandleIgnoreList(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            client.Send(new IgnoredListMessage(new IgnoredInformations[0]));
        }

        //6072
        public static void HandleCharacterSelectedForceReadyMessage(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            Out.Debug(CharSelectActions.Char.name, "NAME");
            client.Send(new CharacterSelectedSuccessMessage(new CharacterBaseInformations(
                1,
                200,
                "TEST",
                CharSelectActions.Char.entityLook,
                1,
                false
                )));
        }

        //5607
        public static void HandleClientKey(BigEndianReader reader, WorldClient client, WorldServer server)
        {

        }

        //250
        public static void HandleGameContextCreate(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            client.Send(new GameContextDestroyMessage());
            client.Send(new GameContextCreateMessage((sbyte)GameContextEnum.ROLE_PLAY));
            client.Send(new CurrentMapMessage(2323));
        }
        //225
        public static void HandleMapInfosRequest(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            client.Send(new MapComplementaryInformationsDataMessage(
                95,
                2323,
                0,
                new HouseInformations[0],
                new GameRolePlayActorInformations[0],
                new InteractiveElement[0],
                new StatedElement[0],
                new MapObstacle[0],
                new FightCommonInformations[0]
                ));

            GameRolePlayActorInformations player = new GameRolePlayActorInformations(
                1,
                CharSelectActions.Char.entityLook,
                new EntityDispositionInformations(200, 1));
            client.Send(new GameRolePlayShowActorMessage(player));
        }
        //890
        public static void HandleChannelEnablingMessage(BigEndianReader reader, WorldClient client, WorldServer server)
        {

        }
        //950 Move
        public static void HandleMove(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            GameMapMovementRequestMessage message = new GameMapMovementRequestMessage();
            message.Unpack(reader);
            client.Send(new GameMapMovementMessage(message.keyMovements, 1));
        }

        //CONSOLE MSG
        public static void HandleConsole(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            AdminCommandMessage message = new AdminCommandMessage();
            message.Unpack(reader);
            string[] args = message.content.Split(' ');
            switch (args[0])
            {
                case "ADDITEM":
                    int Id = int.Parse(args[1]);
                    client.Send(new ObjectAddedMessage(new ObjectItem(
                        63,
                        (short)Id,
                        0,
                        false,
                        new ObjectEffect[0],
                        0,
                        1
                        )));
                    break;
                case "TPMAP":
                    client.Send(new CurrentMapMessage(int.Parse(args[1])));
                    break;
                default:
                    break;
            }
        }
        //5662
        public static void HandleTP(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            AdminQuietCommandMessage message = new AdminQuietCommandMessage();
            message.Unpack(reader);
            if (message.content.Contains("moveto"))
            {
                int position = int.TryParse(message.content.Remove(0, 6), out int pos) ? int.Parse(message.content.Remove(0, 6)) : -1;
                if (position != -1)
                {
                    client.Send(new CurrentMapMessage(position));
                }
            }
        }
    }
}
