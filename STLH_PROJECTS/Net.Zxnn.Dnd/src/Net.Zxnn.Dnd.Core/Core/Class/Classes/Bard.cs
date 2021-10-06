using System;

namespace Net.Zxnn.Dnd.Core.Class.Classes
{
    public class Bard : DndClass
    {
        public override Dice HitDice => Dice.d8;
        public override int GetHitPoints(int level, int constitutionModifier)
        {
            if (level == 1)
            {
                return 8 + constitutionModifier;
            }
            else
            {
                return  Math.Max(HitDice.Roll(), 5) + constitutionModifier;
            }
        }

        public int GetUnarmoredDefense(int dexterityModifier, int constitutionModifier)
        {
            return 10 + dexterityModifier + constitutionModifier;
        }
    } 
}