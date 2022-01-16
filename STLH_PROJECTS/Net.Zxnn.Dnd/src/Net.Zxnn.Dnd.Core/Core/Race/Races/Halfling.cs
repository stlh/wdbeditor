using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Net.Zxnn.Dnd.Core.Race.Races
{
    public class Halfling : DndRace
    {
        public Halfling()
        {
            _abilityScoresIncrease = new AbilityScores(0, 2);
        }
        
        public override int Age => throw new NotImplementedException();
        
        public override RaceSize Size => RaceSize.Small;
        
        public override double Speed  => 25d;
        
        public override ISet<Language> Languages => new HashSet<Language>() {
            Language.Common,
            Language.Halfling
        };

        public override IProficiencies Proficiencies => new ImmutableProficiencies() {};
    }
}