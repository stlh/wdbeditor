using System.Collections.Generic;
using System.Collections.Immutable;

namespace Net.Zxnn.Dnd.Core;

public class ImmutableProficiencies : IProficiencies
{
    public ISet<string> Armor { get => ImmutableHashSet<string>.Empty; init => throw new System.NotImplementedException(); }
    public ISet<string> Weapons { get => ImmutableHashSet<string>.Empty; init => throw new System.NotImplementedException(); }
    public ISet<string> Tools { get => ImmutableHashSet<string>.Empty; init => throw new System.NotImplementedException(); }
    public ISet<string> SavingThrows { get => ImmutableHashSet<string>.Empty; init => throw new System.NotImplementedException(); }
    public ISet<string> Skills { get => ImmutableHashSet<string>.Empty; init => throw new System.NotImplementedException(); }
}