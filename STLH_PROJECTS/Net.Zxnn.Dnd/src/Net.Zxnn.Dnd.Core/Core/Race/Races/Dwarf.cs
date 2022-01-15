using System;
using System.Collections.Generic;

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

        public override ISet<Proficiency> WeaponTraining => new HashSet<Proficiency> {
            Proficiency.BattleAxe,
            Proficiency.HandAxe,
            Proficiency.LightHammer,
            Proficiency.WarHammer
        };
    }
}