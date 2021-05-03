using RailEmu.Common.Utils;
using RailEmu.Protocol.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.World.Network.Handler
{
    public class WorldPacketHandler
    {
        public static Dictionary<uint, Action<BigEndianReader, WorldClient, WorldServer>> packetList = new Dictionary<uint, Action<BigEndianReader, WorldClient, WorldServer>>
        {
            //WORLD
            {110,   BasicActions.HandleAuthenticationTicket},
            {150,   CharSelectActions.HandleCharacterList},
            {160,   CharSelectActions.HandleCharacterCreation},
            {165,   CharSelectActions.HandleCharacterRemove},
            {152,   CharSelectActions.HandleConnexion},
            {4001,  TestActions.HandleFriendsList},
            {5676,  TestActions.HandleIgnoreList},
            {5607,  TestActions.HandleClientKey},
            {250,   TestActions.HandleGameContextCreate},
            {225,   TestActions.HandleMapInfosRequest},
            {890,   TestActions.HandleChannelEnablingMessage},
            {6072,  TestActions.HandleCharacterSelectedForceReadyMessage},
            {5623,  TestActions.HandleQuestList},   
            {950,   TestActions.HandleMove},
            {76,    TestActions.HandleConsole},
            {5662,  TestActions.HandleTP}
        };

        public static void HandlePacket(byte[] data, WorldClient client, WorldServer server)
        {
            BigEndianReader reader = new BigEndianReader(data);
            short header = reader.ReadShort();
            uint Id = (uint)header >> 2;
            uint Length = (uint)header & 3;
            reader = UpdateReader(reader, Length);
            if (!packetList.ContainsKey(Id))
            {
                Out.Warn($"Unexpected packet from { client.Ip}. Invalid packet ID[{ Id}]" +
                        "\nDisconnecting client");
                return;
            }
            Out.Debug($"{"New Packet",-12} : {packetList[Id].Method.Name.Remove(0, 6),30}[{Id,4}] <-- {client.Ip,-12}");
            packetList[Id](reader, client, server);
        }

        public static BigEndianReader UpdateReader(BigEndianReader reader, uint Length)
        {
            #region switch Length
            switch (Length)
            {
                case 0:
                    Length = 0;
                    break;
                case 1:
                    Length = reader.ReadByte();
                    break;
                case 2:
                    Length = reader.ReadUShort();
                    break;
                case 3:
                    Length = (uint)(((reader.ReadByte() & 255) << 16) + ((reader.ReadByte() & 255) << 8) + (reader.ReadByte() & 255));
                    break;
                default:
                    Length = 0;
                    break;
            }
            return reader;
            #endregion
        }
    }
}
