using System;

namespace Net.Zxnn.Dnd.Core.Race.Races
{
    public class Gnome : DndRace
    {
        public Gnome()
        {
            _abilityScoresIncrease = new AbilityScores(0, 0, 0, 2);
        }
        public override int Age => throw new NotImplementedException();
        public override RaceSize Size => RaceSize.Small;
        public override double Speed  => 25d;
        public override string Languages => throw new NotImplementedException();
    }
}