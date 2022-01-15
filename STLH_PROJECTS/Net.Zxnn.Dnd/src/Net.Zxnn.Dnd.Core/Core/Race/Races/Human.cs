using System;
using System.Collections.Generic;

namespace Net.Zxnn.Dnd.Core.Race.Races
{
    public class Human : DndRace
    {
        public Human()
        {
            _abilityScoresIncrease = new AbilityScores(1, 1, 1, 1, 1, 1);
        }
        public override int Age => throw new NotImplementedException();
        public override RaceSize Size => RaceSize.Medium;
        public override double Speed  => 30d;
        public override ISet<Language> Languages => new HashSet<Language> {
            Language.Common
        };
        public override ISet<Proficiency> WeaponTraining => new HashSet<Proficiency> {
        };
    }
}