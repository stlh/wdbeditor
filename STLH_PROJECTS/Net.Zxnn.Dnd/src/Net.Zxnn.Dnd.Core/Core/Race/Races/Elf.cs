using System;
using System.Collections.Generic;

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

        public override ISet<Proficiency> WeaponTraining => new HashSet<Proficiency> {
            Proficiency.LongSword,
            Proficiency.ShortSword,
            Proficiency.ShortBow,
            Proficiency.LongBow
        };
    }
}