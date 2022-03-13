using System;
using System.Collections.Immutable;

namespace Net.Zxnn.Dnd.Core.Class.Classes;
public class Barbarian : DndClass
{
    public Barbarian() : base("Barbarian")
    {

    }

    public override Dice HitDice => Dice.d12;
    public override int GetHitPoints(int level, int constitutionModifier)
    {
        if (level == 1)
        {
            return HitDice.Max + constitutionModifier;
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

    public override IProficiencies Proficiencies => new ImmutableProficiencies() {
        Armor = ImmutableHashSet.Create<string>("LightArmor", "MediumArmor", "Shields"),
        Weapons = ImmutableHashSet.Create<string>("SimpleWeapons", "MartialWeapons"),
        SavingThrows = ImmutableHashSet.Create<string>("Strength", "Constitution")
    };
}