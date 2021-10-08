using System;

namespace Net.Zxnn.Dnd.Core.Race.Races
{
    public class Dwarf : DndRace
    {
        public Dwarf()
        {
            _abilityScoresIncrease = new AbilityScores(2);
        }
        public override int Age => throw new NotImplementedException();
        public override RaceSize Size => RaceSize.Medium;
        public override double Speed  => 25d;
        public override string Languages => throw new NotImplementedException();
    }
}