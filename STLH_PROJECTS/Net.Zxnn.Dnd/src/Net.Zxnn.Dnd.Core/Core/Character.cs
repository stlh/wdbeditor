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
            CombatService.attack(this, target);
        }

        public void move()
        {
            Console.WriteLine($"{Name} is moving.");
        }
    }
}