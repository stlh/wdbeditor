using System;
using System.Collections.Immutable;

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
        
        public int ExperiencePoints { get; set; }

        public IProficiencies Proficiencies
        {
            get {
                return  new ImmutableProficiencies() {
                    Armor = ImmutableHashSet<string>.Empty,
                    Weapons = ImmutableHashSet.Create<string>("BattleAxe", "HandAxe", "LightHammer", "WarHammer"),
                    Tools = ImmutableHashSet<string>.Empty,
                    SavingThrows = ImmutableHashSet<string>.Empty,
                    Skills = ImmutableHashSet<string>.Empty
                };
            }
        }

        public int WeaponProficiencyBonus
        {
            get
            {
                return this.Proficiencies.Weapons.Contains(this.EquipmentSockets.Weapon.Name) ? this.ProficiencyBonus : 0;
            }
        }

        public int ProficiencyBonus
        {
            get
            {
                return AbilityTools.GetProficiencienBonus(this.Level);
            }
        }

        public void attack(Character target)
        {
            throw new NotImplementedException();
        }

        public void move()
        {
            Console.WriteLine($"{Name} is moving.");
        }
    }
}