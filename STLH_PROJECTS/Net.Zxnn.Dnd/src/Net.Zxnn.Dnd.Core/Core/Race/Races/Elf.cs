using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Net.Zxnn.Dnd.Core.Race.Races
{
    public class Elf : DndRace
    {
        public Elf()
        {
            _abilityScoresIncrease = new AbilityScores(0, 2);
        }
        public override int Age => throw new NotImplementedException();
        public override RaceSize Size => RaceSize.Medium;
        public override double Speed  => 30d;
        public override ISet<Language> Languages => new HashSet<Language> {
            Language.Common,
            Language.Elvish
        };

        public override IProficiencies Proficiencies => new ImmutableProficiencies() {
            Weapons = ImmutableHashSet.Create<string>("LongSword", "ShortSword", "ShortBow", "LongBow"),
        };
    }
}