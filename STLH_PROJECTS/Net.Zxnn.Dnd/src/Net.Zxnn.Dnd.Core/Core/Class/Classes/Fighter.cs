using System;

namespace Net.Zxnn.Dnd.Core.Class.Classes
{
    public class Fighter : DndClass
    {
        public override Dice HitDice => Dice.d10;

        public override int GetHitPoints(int level, int constitutionModifier)
        {
            if (level == 1)
            {
                return HitDice.Max;
            }
            else
            {
                return HitDice.Roll() + constitutionModifier;
            }
        }
    }
}