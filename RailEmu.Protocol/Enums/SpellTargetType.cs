using System;
namespace RailEmu.Protocol.Enums
{
	public enum SpellTargetType
	{
		NONE = 0,
		SELF = 1,
		ALLY_SUMMONS = 8,
		ALLY_ALL = 255,
		ENNEMY_SUMMONS = 1024,
        ENEMY_ALL = 32512,
		ALL = 32767,
		ALL_SUMMONS = 3096,
		ONLY_SELF = 32768,
        WITHOUT_SELF = 32766
    }
}
