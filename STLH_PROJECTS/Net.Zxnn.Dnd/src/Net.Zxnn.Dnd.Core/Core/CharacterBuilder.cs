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
        private Abilities abilities;
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

        public CharacterBuilder WithAbilities(Abilities abilities)
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
                c.Abilities = new Abilities() {
                    Strength = 15,
                    Dexterity = 12,
                    Constitution = 14,
                    Intelligence = 13,
                    Wisdom = 10,
                    Charisma = 8
                };
            }
            else {
                c.Abilities = this.abilities;
            }

            c.HitPointMax = DiceBox.MaxOfDice(c.Class.HitDice) + AbilityTools.GetAbilityModifier(c.Abilities.Constitution);
            c.HitPoints = c.HitPointMax;

            return c;
        }
    }
}