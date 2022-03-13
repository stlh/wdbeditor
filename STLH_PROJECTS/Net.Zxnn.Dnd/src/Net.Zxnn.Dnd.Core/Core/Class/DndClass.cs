using System;
using Net.Zxnn.Dnd.Core.Class.Classes;

namespace Net.Zxnn.Dnd.Core.Class;

public abstract class DndClass
{
    public String Name { get; init; }

    public DndClass(String name)
    {
        this.Name = name;
    }

    public static readonly DndClass Fighter = new Fighter();
    
    public static readonly DndClass Barbarian = new Barbarian();
    
    public abstract Dice HitDice { get; }

    public abstract int GetHitPoints(int level, int constitutionModifier);

    public abstract IProficiencies Proficiencies { get; }
}