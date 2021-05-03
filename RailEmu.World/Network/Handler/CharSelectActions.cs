using RailEmu.Common.Utils;
using RailEmu.Protocol.Enums;
using RailEmu.Protocol.IO;
using RailEmu.Protocol.Messages;
using RailEmu.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RailEmu.World.Network.Handler
{
    public static class CharSelectActions
    {
        public static CharacterBaseInformations Char { get; set; }

        public static void HandleCharacterList(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            if(Char != null)
            {
                client.Send(new CharactersListMessage(false, new CharacterBaseInformations[] { Char }));
                Out.Debug(Char.sex.ToString() + Char.breed.ToString(), "Infos");
                return;
            }
            client.Send(new CharactersListMessage(false, new List<CharacterBaseInformations>()));
            //TODO : Get list of characters and send them
        }

        public static void HandleCharacterCreation(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            try
            {
                CharacterCreationRequestMessage message = new CharacterCreationRequestMessage();
                message.Unpack(reader);
                client.Send(new CharacterCreationResultMessage(0));
                Thread.Sleep(1000);
                int y = 0;
                int[] colors = new int[5];
                foreach (int x in message.colors)
                    if (y < 5 && x != -1)
                    {
                        colors[y] = (y + 1 & 255) << 24 | x & 16777215;
                        //((nb & 255) << 24 | color & 16777215)
                        y++;
                    }
                    else
                        break;

                for(int x = 0; x < colors.Length; x++)
                {
                    Out.Debug(colors[x].ToString(), "Colors");
                }
                //x => x.Key << 24 | x.Value.ToArgb() & 0xFFFFFF

                int style = message.breed * 10;
                if (message.sex)
                    style++;

                Char = new CharacterBaseInformations(
                        1,
                        200,
                        message.name,
                        new EntityLook(1, new short[] { (short)style }, colors, new short[] { 125 }, new SubEntity[] { }),
                        message.breed,
                        message.sex);

                IEnumerable<CharacterBaseInformations> chars = new List<CharacterBaseInformations>()
                {
                Char
                };
                
                client.Send(new CharactersListMessage(false, chars));
            }
            catch(Exception e)
            {
                Out.Error(e.Message);
            }
            //var_character =
            //{
            //    Level = 1,
            //    Name = message.name,
            //    Breed = message.breed,
            //    Sex = message.sex,
            //    EntityLook = EntityManager.Instance.BuildEntityLook(message.breed, message.sex, message.colors.ToList()),
            //    MapId = BreedManager.Instance.GetStartMap(message.breed),
            //    CellId = BreedManager.Instance.GetStartCell(message.breed),
            //    Direction = BreedManager.Instance.GetStartDirection(message.breed),
            //    SpellsPoints = 1
            //};
        }

        public static void HandleCharacterRemove(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            Char = null;
            client.Send(new CharactersListMessage(false, new List<CharacterBaseInformations>()));
        }
        public static void HandleConnexion(BigEndianReader reader, WorldClient client, WorldServer server)
        {
            client.Send(new CharacterSelectedSuccessMessage(new CharacterBaseInformations(
                Char.id,
                Char.level,
                Char.name,
                Char.entityLook,
                Char.breed,
                Char.sex
                )));
            client.Send(new InventoryContentMessage(new ObjectItem[1]{ new ObjectItem(0,0,0,true,new ObjectEffect[0], 7754, 1) }, 5000000));   //-> Items enumerable + kamas
            client.Send(new ShortcutBarContentMessage(1, new Shortcut[0]));
            client.Send(new ShortcutBarContentMessage(2, new Shortcut[0]));
            client.Send(new EmoteListMessage(new sbyte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 19, 21, 22, 23, 24 }));
            client.Send(new EnabledChannelsMessage(new sbyte[] { 0, 1, 2, 3, 4, 5, 6, 8, 9, 10, 11 }, new sbyte[0]));
            client.Send(new AlignmentRankUpdateMessage(0, false));
            client.Send(new AlignmentSubAreasListMessage(new short[0], new short[0]));
            client.Send(new SpellListMessage(true, new SpellItem[0]));              //-> Prev / SpellItem list
            client.Send(new InventoryWeightMessage(0, 10000));                      //-> Weigth / Weigth max
            #region
            client.Send(new CharacterStatsListMessage(new CharacterCharacteristicsInformations(
                0,
                0,
                1000,
                5000000,
                5000,
                50,
                new ActorExtendedAlignmentInformations(0, 0, 0, 0, 0, 0, 0, 0, false),
                5000,
                5000,
                5000,
                5000,
                12,
                6,
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                0,
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterBaseCharacteristic(0, 0, 0, 0),
                new CharacterSpellModification[0]
                )));
            #endregion big packet
            client.Send(new JobDescriptionMessage(new JobDescription[0]));
            client.Send(new JobExperienceMultiUpdateMessage(new JobExperience[0]));
            client.Send(new JobCrafterDirectorySettingsMessage(new JobCrafterDirectorySettings[0]));
            client.Send(new TextInformationMessage(1, 89, new string[0]));
            client.Send(new LifePointsRegenBeginMessage(10));
            client.Send(new SetCharacterRestrictionsMessage(new ActorRestrictionsInformations(false, false, false, true, false, false, false, false, false, false, false, false, false, true, true, true, false, false, false, false, false)));



        }
        public static EntityLook BuildEntityLook(string entityLook)
        {
            string[] lookStringSplit = entityLook.Replace("{", "").Replace("}", "").Split('|');
            short bonesId = short.Parse(lookStringSplit[0]);
            short[] skins;
            if (lookStringSplit[1].Contains(","))
            {
                string[] skinsString = lookStringSplit[1].Split(',');
                skins = new short[skinsString.Length];
                for (int i = 0; i < skinsString.Length; i++)
                {
                    skins[i] = short.Parse(skinsString[i]);
                }
            }
            else
            {
                skins = string.IsNullOrEmpty(lookStringSplit[1])
                    ? new short[0]
                    : new[] { short.Parse(lookStringSplit[1]) };
            }
            int[] colors;
            if (lookStringSplit[2].Contains(","))
            {
                string[] colorsString = lookStringSplit[2].Split(',');
                colors = new int[colorsString.Length];
                for (int i = 0; i < colorsString.Length; i++)
                {
                    int color = int.Parse(colorsString[i].Remove(0, 2));
                    if (color != -1)
                    {
                        colors[i] = (i + 1 & 255) << 24 | color & 16777215;
                    }
                }
            }
            else
            {
                colors = new int[0];
            }
            short[] size = { short.Parse(lookStringSplit[3]) };
            SubEntity[] subEntity;
            if (lookStringSplit.Length > 4) //if contains subEntity
            {
                string subEntitiesString = entityLook.Substring(entityLook.IndexOf('@') - 1);
                if (subEntitiesString.Count(@char => @char == '@') > 1) //more than one subEntity
                {
                    string[] subEntityString = subEntitiesString.Split(new[] { "}," },
                        StringSplitOptions.RemoveEmptyEntries);
                    subEntity = new SubEntity[subEntityString.Length];
                    for (int i = 0; i < subEntityString.Length; i++)
                    {
                        string[] strings = subEntityString[i].Replace("{", "").Replace("}", "").Split('|');
                        short subEntityBonesId = short.Parse(strings[0].Substring((4)));
                        short[] subEntitySkins;
                        if (strings[1].Contains(","))
                        {
                            string[] subEntitySkinString = strings[1].Split(',');
                            subEntitySkins = new short[subEntitySkinString.Length];
                            for (int j = 0; j < subEntitySkinString.Length; j++)
                            {
                                subEntitySkins[j] = short.Parse(subEntitySkinString[j]);
                            }
                        }
                        else
                        {
                            subEntitySkins = string.IsNullOrEmpty(strings[1])
                                ? new short[0]
                                : new[] { short.Parse(strings[1]) };
                        }
                        int[] subEntityColors;
                        if (strings[2].Contains(","))
                        {
                            string[] subEntityColorsString = strings[2].Split(',');
                            subEntityColors = new int[subEntityColorsString.Length];
                            for (int k = 0; k < subEntityColorsString.Length; k++)
                            {
                                int color = int.Parse(subEntityColorsString[k].Remove(0, 2));
                                if (color != -1)
                                {
                                    subEntityColors[k] = (k + 1 & 255) << 24 | color & 16777215;
                                }
                            }
                        }
                        else
                        {
                            subEntityColors = new int[0];
                        }
                        short[] subEntitySize = { short.Parse(strings[3]) };
                        subEntity[i] = new SubEntity(sbyte.Parse(strings[0][0].ToString()),
                            sbyte.Parse(strings[0][2].ToString()),
                            new EntityLook(subEntityBonesId, subEntitySkins, subEntityColors,
                                subEntitySize, new SubEntity[0]));
                    }
                }
                else //one subEntity
                {
                    string[] oneSubEntityString = subEntitiesString.Replace("{", "").Replace("}", "").Split('|');
                    short[] oneSubEntitySkins;
                    if (oneSubEntityString[1].Contains(","))
                    {
                        string[] oneSubEntitySkinString = oneSubEntityString[1].Split(',');
                        oneSubEntitySkins = new short[oneSubEntitySkinString.Length];
                        for (int i = 0; i < oneSubEntitySkinString.Length; i++)
                        {
                            oneSubEntitySkins[i] = short.Parse(oneSubEntitySkinString[i]);
                        }
                    }
                    else
                    {
                        oneSubEntitySkins = string.IsNullOrEmpty(oneSubEntityString[1])
                            ? new short[0]
                            : new[] { short.Parse(oneSubEntityString[1]) };
                    }
                    int[] oneSubEntityColors;
                    if (oneSubEntityString[2].Contains(","))
                    {
                        string[] oneSubEntityColorsString = oneSubEntityString[2].Split(',');
                        oneSubEntityColors = new int[oneSubEntityColorsString.Length];
                        for (int j = 0; j < oneSubEntityColorsString.Length; j++)
                        {
                            int color = int.Parse(oneSubEntityColorsString[j].Remove(0, 2));
                            if (color != -1)
                            {
                                oneSubEntityColors[j] = (j + 1 & 255) << 24 | color & 16777215;
                            }
                        }
                    }
                    else
                    {
                        oneSubEntityColors = new int[0];
                    }
                    short[] oneSubEntitySize = { short.Parse(oneSubEntityString[3]) };
                    subEntity = new[]
                    {
                        new SubEntity(sbyte.Parse(oneSubEntityString[0][0].ToString()),
                            sbyte.Parse(oneSubEntityString[0][2].ToString()),
                            new EntityLook( short.Parse(oneSubEntityString[0].Substring((4))), oneSubEntitySkins, oneSubEntityColors,
                                oneSubEntitySize, new SubEntity[0]))
                    };
                }
            }
            else //no subEntity
            {
                subEntity = new SubEntity[0];
            }
            return new EntityLook(bonesId, skins, colors, size, subEntity);
        }
    }
}
