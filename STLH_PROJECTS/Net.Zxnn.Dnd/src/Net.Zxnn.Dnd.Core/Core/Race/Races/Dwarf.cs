using System;

namespace Net.Zxnn.Dnd.Core.Race.Races
{
    public class Dwarf : DndRace
    {
        public Dwarf()
        {
            _abilityScoreIncrease = new AbilityScoreIncrease(2, 0, 0, 0, 0, 0);
        }
        public override int Age => throw new NotImplementedException();
        public override int Alignment => 0;
        public override RaceSize Size => RaceSize.Medium;
        public override double Speed  => 25d;
        public override string Languages => throw new NotImplementedException();
    }
}