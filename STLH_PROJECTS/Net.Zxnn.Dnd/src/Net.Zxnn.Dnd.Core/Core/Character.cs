using System;

using Net.Zxnn.Dnd.Core.Class;
using Net.Zxnn.Dnd.Core.Race;

namespace Net.Zxnn.Dnd.Core
{
    public class Character : ICharacter
    {
        public Character()
        {
        }

        public string Name { get; set; }

        public DndRace Race { get; set; }

        public DndClass Class { get; set; }

        public int Level { get; set; }

        public AbilityScores AbilityScores { get; set; }

        public int ArmorClass {
            get {
                // Dexterity Modifier + Armor Modifier
                return this.EquipmentSockets?.Armor?.ArmorClass ?? 10
                    + this.EquipmentSockets?.Shield?.ArmorClass ?? 0
                    + AbilityTools.GetAbilityModifier(this.AbilityScores.Dexterity);
            }
        }

        public EquipmentSockets EquipmentSockets { get; set; }

        public int HitPoints { get; set; }
        
        public int HitPointMax { get; set; }
        
        public int ExperlencePoints { get; set; }

        public void attack(Character target)
        {
            int hit = Dice.d20.Roll();
            int abilityModifier = AbilityTools.GetAbilityModifier(this.AbilityScores?.Strength ?? 10);

            Console.WriteLine($"{this.Name} roll(d20): {hit} + {abilityModifier}");

            switch (hit) {
                case 20:
                break;
                case 1:
                break;
                default:
                break;
            }

            Boolean isNotMiss = hit + abilityModifier >= target.ArmorClass;
            
            if (isNotMiss) {
                int point1 = DiceBox.Roll(this.EquipmentSockets?.Weapon.DiceType);
                int point2 = point1 + abilityModifier;
                
                Console.WriteLine($"{Name} use {this.EquipmentSockets?.Weapon.Name} hit roll({this.EquipmentSockets?.Weapon.DiceType}) {point1} + {abilityModifier} total {point2}");
                Console.WriteLine($"{target.Name} been hit {point2}, hit points: {target.HitPoints -= point2}");

                if (target.HitPoints <= 0) {
                    target.HitPoints = 0;
                    Console.WriteLine($"{target.Name} lose consciousness");
                }
            }
            else {
                Console.WriteLine($"{Name} is miss");
            }
        }

        public void move()
        {
            Console.WriteLine($"{Name} is moving.");
        }
    }
}