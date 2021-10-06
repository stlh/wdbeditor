using System;

namespace Net.Zxnn.Dnd.Core.Race.Races
{
    public class Dragonborn : DndRace
    {
        public Dragonborn()
        {
            _abilityScoreIncrease = new AbilityScoreIncrease(2, 0, 0, 0, 0, 1);
        }
        public override int Age => throw new NotImplementedException();
        public override RaceSize Size => RaceSize.Medium;
        public override double Speed  => 30d;
        public override string Languages => throw new NotImplementedException();
    }
}