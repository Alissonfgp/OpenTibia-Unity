﻿namespace OpenTibiaUnity.Core.Communication.Game
{
    internal partial class ProtocolGame : Internal.Protocol
    {
        private void ParsePreyFreeListRerollAvailability(Internal.ByteArray message) {
            byte slot = message.ReadUnsignedByte();
            ushort minutes = message.ReadUnsignedShort();
        }

        private void ParsePreyTimeLeft(Internal.ByteArray message) {
            byte slot = message.ReadUnsignedByte();
            ushort minutes = message.ReadUnsignedShort();
        }

        private void ParsePreyData(Internal.ByteArray message) {
            int slot = message.ReadUnsignedByte();
            var state = message.ReadEnum<PreySlotStates>();
            switch (state) {
                case PreySlotStates.Locked: {
                    message.ReadEnum<PreySlotUnlockType>();
                    break;
                }

                case PreySlotStates.Inactive: {
                    break;
                }

                case PreySlotStates.Active: {
                    string monsterName = message.ReadString();
                    var monsterOutfit = ReadCreatureOutfit(message);
                    var bonusType = message.ReadEnum<PreyBonusTypes>();
                    int bonusValue = message.ReadUnsignedShort();
                    int bonusGrade = message.ReadUnsignedByte();
                    int timeLeft = message.ReadUnsignedShort();
                    break;
                }

                case PreySlotStates.Selection: {
                    byte size = message.ReadUnsignedByte();
                    for (int i = 0; i < size; i++) {
                        string monsterName = message.ReadString();
                        var monsterOutfit = ReadCreatureOutfit(message);
                    }
                    break;
                }

                case PreySlotStates.SelectionChangeMonster: {
                    var bonusType = message.ReadEnum<PreyBonusTypes>();
                    int bonusValue = message.ReadUnsignedShort();
                    int bonusGrade = message.ReadUnsignedByte();
                    byte size = message.ReadUnsignedByte();
                    for (int i = 0; i < size; i++) {
                        string monsterName = message.ReadString();
                        var monsterOutfit = ReadCreatureOutfit(message);
                    }
                    break;
                }
                default:
                    break;
            }

            message.ReadUnsignedShort(); // timeUntilFreeListReroll
            if (OpenTibiaUnity.GameManager.ClientVersion >= 1190) {
                message.ReadUnsignedByte(); // preyWildCards
            }
        }

        private void ParsePreyPrices(Internal.ByteArray message) {
            message.ReadUnsignedInt(); // rerollPrice in gold
            if (OpenTibiaUnity.GameManager.ClientVersion >= 1190) {
                message.ReadUnsignedByte(); // unknown
                message.ReadUnsignedByte(); // selectCreatureDirectly in preyWildCards
            }
        }
    }
}