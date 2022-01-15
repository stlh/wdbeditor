using System;
using System.Collections.Generic;

namespace Net.Zxnn.Dnd.Core.Race.Races
{
    public class Dragonborn : DndRace
    {
        public Dragonborn()
        {
            _abilityScoresIncrease = new AbilityScores(2, 0, 0, 0, 0, 1);
        }
        public override int Age => throw new NotImplementedException();
        public override RaceSize Size => RaceSize.Medium;
        public override double Speed  => 30d;
        public override ISet<Language> Languages => new HashSet<Language> {
            Language.Common,
            Language.Draconic
        };
        public override ISet<Proficiency> WeaponTraining => new HashSet<Proficiency> {
        };
    }
}