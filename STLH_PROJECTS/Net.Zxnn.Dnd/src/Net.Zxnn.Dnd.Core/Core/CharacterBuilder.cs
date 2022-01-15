using System;

using Net.Zxnn.Dnd.Core.Class;
using Net.Zxnn.Dnd.Core.Class.Classes;
using Net.Zxnn.Dnd.Core.Race;
using Net.Zxnn.Dnd.Core.Race.Races;

namespace Net.Zxnn.Dnd.Core
{
    public class CharacterBuilder
    {
        private string name;
        private DndRace race;
        private DndClass clazz;
        private AbilityScores abilities;
        public CharacterBuilder(){

        }

        public CharacterBuilder WithName(string name)
        {
            this.name = name;

            return this;
        }

        public CharacterBuilder WithRace(DndRace race)
        {
            this.race = race;

            return this;
        }

        public CharacterBuilder WithAbilities(AbilityScores abilities)
        {
            this.abilities = abilities;

            return this;
        }

        public CharacterBuilder WithClass(DndClass clazz)
        {
            this.clazz = clazz;

            return this;
        }

        public Character Build()
        {
            Character c = new Character();

            c.Name = this.name;
            c.EquipmentSockets = new EquipmentSockets();

            if (this.race == null)
            {
                throw new Exception("must choose a race");
            }

            if (this.clazz == null)
            {
                throw new Exception("must choose a class");
            }

            c.Race = this.race;

            c.Class = this.clazz;

            c.Level = 1;
            
            if (this.abilities == null) {
                throw new Exception("abilities cannot null");
            }
            c.AbilityScores = this.abilities + c.Race.AbilityScoresIncrease;

            c.HitPointMax = c.Class.HitDice.Max + AbilityTools.GetAbilityModifier(c.AbilityScores.Constitution);
            c.HitPoints = c.HitPointMax;

            return c;
        }
    }
}