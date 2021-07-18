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

            character.Level = 1;
            
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

            character.HitPointMax = DiceBox.MaxOfDice(character.Class.HitDice) + AbilityTools.GetAbilityModifier(character.Abilities.Constitution);
            character.HitPoints = character.HitPointMax;

            return character;
        }
    }
}