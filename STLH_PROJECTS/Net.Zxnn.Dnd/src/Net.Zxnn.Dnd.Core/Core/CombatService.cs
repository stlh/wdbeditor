using System;

using Net.Zxnn.Dnd.Core.Class;
using Net.Zxnn.Dnd.Core.Race;

namespace Net.Zxnn.Dnd.Core
{
    public class CombatService
    {
        public CombatService()
        {

        }

        public static void attack(Character a, Character target)
        {
            // Attack Rolls
            int attackRoll = Dice.d20.Roll();
            int abilityModifier = AbilityTools.GetAbilityModifier(a.AbilityScores?.Strength ?? 10);
            // TODO: attacher's proficiency bonus
            int proficiencyBonus = 0;

            Console.WriteLine($"{a.Name} roll(d20): {attackRoll} + {abilityModifier} + {proficiencyBonus}");

            bool isCriticalHit = false;
            bool isMiss = false;

            switch (attackRoll) {
                case 20:
                    Console.WriteLine("!!!Critical!!!");
                    isCriticalHit = true;
                    break;
                case 1:
                    isMiss = true;
                    break;
                default:
                    isMiss = attackRoll + abilityModifier + proficiencyBonus < target.ArmorClass;
                    break;
            }
            
            int weaponPoints;
            if (isCriticalHit)
            {
                weaponPoints = DiceBox.Roll(a.EquipmentSockets?.Weapon.DiceType) + DiceBox.Roll(a.EquipmentSockets?.Weapon.DiceType);
            }
            
            if (isMiss)
            {
                Console.WriteLine($"{a.Name} is miss");
                return;
            }
            else
            {
                weaponPoints = DiceBox.Roll(a.EquipmentSockets?.Weapon.DiceType);
            }

            int totalPoints = weaponPoints +  abilityModifier;
            
            Console.WriteLine($"{a.Name} use {a.EquipmentSockets?.Weapon.Name} hit roll({a.EquipmentSockets?.Weapon.DiceType}) {weaponPoints} + {abilityModifier} total {totalPoints}");
            Console.WriteLine($"{target.Name} been hit {totalPoints}, hit points: {target.HitPoints -= totalPoints}");

            if (target.HitPoints <= 0) {
                target.HitPoints = 0;
                Console.WriteLine($"{target.Name} Falling Unconscious");
            }
        }
    }
}