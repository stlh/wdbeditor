using System;

namespace Net.Zxnn.Dnd.Core.Race.Races
{
    public class Human : DndRace
    {
        public Human()
        {
            _abilityScoreIncrease = new AbilityScoreIncrease(1, 1, 1, 1, 1, 1);
        }
        public override int Age => throw new NotImplementedException();
        public override RaceSize Size => RaceSize.Medium;
        public override double Speed  => 30d;
        public override string Languages => throw new NotImplementedException();
    }
}