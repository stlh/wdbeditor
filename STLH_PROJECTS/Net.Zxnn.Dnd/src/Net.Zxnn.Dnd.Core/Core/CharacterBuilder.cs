using Net.Zxnn.Dnd.Core.Races;
using Net.Zxnn.Dnd.Core.Classes;

namespace Net.Zxnn.Dnd.Core
{
    public class CharacterBuilder
    {
        private string name;
        private Race race;
        private Class clazz;
        private Abilities abilities;
        public CharacterBuilder(){

        }

        public CharacterBuilder WithName(string name)
        {
            this.name = name;

            return this;
        }

        public CharacterBuilder WithRace(Race race)
        {
            this.race = race;

            return this;
        }

        public CharacterBuilder WithAbilities(Abilities abilities)
        {
            this.abilities = abilities;

            return this;
        }

        public CharacterBuilder WithClass(Class clazz)
        {
            this.clazz = clazz;

            return this;
        }

        public Character Build()
        {
            Character character = new Character();

            character.Name = this.name;
            character.EquipmentSockets = new EquipmentSockets();

            if (this.race == null) {
                character.Race = new Human();
            }
            else {
                character.Race = this.race;
            }

            if (this.clazz == null) {
                character.Class = new Fighter();
            }
            else {
                character.Class = this.clazz;
            }
            
            if (this.abilities == null) {
                character.Abilities = new Abilities() {
                    Strength = 15,
                    Dexterity = 12,
                    Constitution = 14,
                    Intelligence = 13,
                    Wisdom = 10,
                    Charisma = 8
                };
            }
            else {
                character.Abilities = this.abilities;
            }

            return character;
        }
    }
}