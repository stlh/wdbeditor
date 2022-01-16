using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Net.Zxnn.Dnd.Core.Race.Races
{
    public class Dwarf : DndRace
    {
        public Dwarf()
        {
            _abilityScoresIncrease = new AbilityScores(0, 0, 2);
        }
        
        public override int Age => throw new NotImplementedException();
        
        public override RaceSize Size => RaceSize.Medium;
        
        public override double Speed  => 25d;
        
        public override ISet<Language> Languages => new HashSet<Language> {
            Language.Common,
            Language.Dwarvish
        };

        public override IProficiencies Proficiencies => new ImmutableProficiencies() {
            Armor = ImmutableHashSet<string>.Empty,
            Weapons = ImmutableHashSet.Create<string>("BattleAxe", "HandAxe", "LightHammer", "WarHammer"),
            Tools = ImmutableHashSet<string>.Empty,
            SavingThrows = ImmutableHashSet<string>.Empty,
            Skills = ImmutableHashSet<string>.Empty
        };
    }
}