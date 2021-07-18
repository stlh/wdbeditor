using System;

namespace Net.Zxnn.Dnd.Core.Race
{
    public abstract class DndRace
    {
        public abstract int AbilityScoreIncrease { get; }
        public abstract int Age { get; }
        public abstract int Alignment { get; }
        public abstract RaceSize Size { get; }
        public abstract double Speed { get; }
        public abstract String Languages { get; }
    }
}