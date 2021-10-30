using System;

using Microsoft.Extensions.Logging;

namespace Net.Zxnn.Dnd.Core
{
    public class CombatService
    {
        private readonly ILogger _logger;

        public CombatService(ILogger<Scene> logger)
        {
            _logger = logger;
        }

        public void attack(Character a, Character target)
        {
            // Attack Rolls
            int attackRoll = Dice.d20.Roll();
            int abilityModifier = AbilityTools.GetAbilityModifier(a.AbilityScores?.Strength ?? 10);
            // TODO: attacher's proficiency bonus
            int proficiencyBonus = 0;

            _logger.LogDebug($"{a.Name} roll(d20): {attackRoll} + {abilityModifier} + {proficiencyBonus}");

            bool isCriticalHit = false;
            bool isMiss = false;

            switch (attackRoll) {
                case 20:
                    _logger.LogDebug("!!!Critical!!!");
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
                _logger.LogDebug($"{a.Name} is miss");
                return;
            }
            else
            {
                weaponPoints = DiceBox.Roll(a.EquipmentSockets?.Weapon.DiceType);
            }

            int totalPoints = weaponPoints +  abilityModifier;
            
            _logger.LogDebug($"{a.Name} use {a.EquipmentSockets?.Weapon.Name} hit roll({a.EquipmentSockets?.Weapon.DiceType}) {weaponPoints} + {abilityModifier} total {totalPoints}");
            _logger.LogDebug($"{target.Name} been hit {totalPoints}, hit points: {target.HitPoints -= totalPoints}");

            if (target.HitPoints <= 0) {
                target.HitPoints = 0;
                _logger.LogDebug($"{target.Name} Falling Unconscious");
            }
        }
    }
}