using System;
using System.Collections.Immutable;

namespace Net.Zxnn.Dnd.Core.Class.Classes
{
    public class Bard : DndClass
    {
        public override Dice HitDice => Dice.d8;
        public override int GetHitPoints(int level, int constitutionModifier)
        {
            if (level == 1)
            {
                return HitDice.Max + constitutionModifier;
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
        public override IProficiencies Proficiencies => new ImmutableProficiencies() {
            Armor = ImmutableHashSet.Create<string>("LightArmor"),
            Weapons = ImmutableHashSet.Create<string>("SimpleWeapons", "HandCrossbows", "LongSwords", "Rapiers", "ShortSwords"),
            SavingThrows = ImmutableHashSet.Create<string>("Dexterity", "Charisma")
        };
    } 
}