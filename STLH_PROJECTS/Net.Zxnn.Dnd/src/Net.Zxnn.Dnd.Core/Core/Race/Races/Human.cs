using System;

namespace Net.Zxnn.Dnd.Core.Race.Races
{
    public class Human : DndRace
    {
        public override int AbilityScoreIncrease => 1;
        public override int Age => throw new NotImplementedException();
        public override int Alignment => 0;
        public override RaceSize Size => RaceSize.Medium;
        public override double Speed  => 30d;
        public override string Languages => throw new NotImplementedException();
    }
}