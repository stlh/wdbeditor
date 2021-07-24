using System;
using Net.Zxnn.Dnd.Core.Race.Races;

namespace Net.Zxnn.Dnd.Core.Race
{
    public abstract class DndRace
    {
        protected AbilityScoreIncrease _abilityScoreIncrease;
        public static readonly DndRace Human = new Human();
        public AbilityScoreIncrease AbilityScoreIncrease => _abilityScoreIncrease;
        public abstract int Age { get; }
        public abstract int Alignment { get; }
        public abstract RaceSize Size { get; }
        public abstract double Speed { get; }
        public abstract String Languages { get; }
    }
}