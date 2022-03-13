using System;
using System.Collections.Immutable;

namespace Net.Zxnn.Dnd.Core.Class.Classes;
public class Fighter : DndClass
{
    public Fighter() : base("Fighter")
    {

    }
    public override Dice HitDice => Dice.d10;

    public override int GetHitPoints(int level, int constitutionModifier)
    {
        if (level == 1)
        {
            return HitDice.Max + constitutionModifier;
        }
        else
        {
            return Math.Max(HitDice.Roll(), 6) + constitutionModifier;
        }
    }

    public override IProficiencies Proficiencies => new ImmutableProficiencies() {
        Armor = ImmutableHashSet.Create<string>("AllArmor","Shields"),
        Weapons = ImmutableHashSet.Create<string>("SimpleWeapons", "MartialWeapons"),
        SavingThrows = ImmutableHashSet.Create<string>("Strength", "Constitution")
    };
}