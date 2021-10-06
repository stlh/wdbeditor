using System;

namespace Net.Zxnn.Dnd.Core.Class.Classes
{
    public class Barbarian : DndClass
    {
        public override Dice HitDice => Dice.d12;
        public override int GetHitPoints(int level, int constitutionModifier)
        {
            if (level == 1)
            {
                return HitDice.Max;
            }
            else
            {
                return Math.Max(Dice.d12.Roll(), 7) + constitutionModifier;
            }
        }

                public int GetUnarmoredDefense(int dexterityModifier, int constitutionModifier)
        {
            return 10 + dexterityModifier + constitutionModifier;
        }
    } 
}