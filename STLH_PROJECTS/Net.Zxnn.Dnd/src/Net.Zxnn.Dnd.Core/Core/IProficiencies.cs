using System.Collections.Generic;
using System.Collections.Immutable;


namespace Net.Zxnn.Dnd.Core;

public interface IProficiencies
{
    ISet<string> Armor { get; init; }

    ISet<string> Weapons { get; init; }

    ISet<string> Tools { get; init; }

    ISet<string> SavingThrows { get; init; }

    ISet<string> Skills { get; init; }
}