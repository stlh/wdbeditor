using System;
using System.Collections.Generic;
using Net.Zxnn.Dnd.Core.Race.Races;

namespace Net.Zxnn.Dnd.Core.Race
{
    public abstract class DndRace
    {
        protected AbilityScores _abilityScoresIncrease;
        public static readonly DndRace Human = new Human();
        public static readonly DndRace Dwarf = new Dwarf();
        public AbilityScores AbilityScoresIncrease => _abilityScoresIncrease;
        public abstract int Age { get; }
        public abstract RaceSize Size { get; }
        public abstract double Speed { get; }
        public abstract ISet<Language> Languages { get; }

        public abstract IProficiencies Proficiencies { get; }
    }
}