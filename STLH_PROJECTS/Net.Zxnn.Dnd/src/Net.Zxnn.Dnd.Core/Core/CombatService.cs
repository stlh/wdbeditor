using System;

using Microsoft.Extensions.Logging;

namespace Net.Zxnn.Dnd.Core;
public class CombatService
{
    private readonly ILogger _logger;

    public CombatService(ILogger<CombatService> logger)
    {
        _logger = logger;
    }

    public void attack(Character a, Character target)
    {
        // Attack Rolls
        int attackRoll = Dice.d20.Roll();

        // Modifiers to the Roll
        int abilityModifier = AbilityTools.GetAbilityModifier(a.AbilityScores?.Strength ?? 10);
        // Proficiency Bonus
        int weaponProficiencyBonus = a.WeaponProficiencyBonus;

        _logger.LogDebug($"{a.Name} roll(d20): {attackRoll} + {abilityModifier} + {weaponProficiencyBonus}");

        bool isCriticalHit = false;
        bool isMiss = false;

        switch (attackRoll) {
            case 20:
                _logger.LogDebug("!!!Critical!!!");
                isCriticalHit = true;
                break;
            case 1:
                _logger.LogDebug("!!!Miss!!!");
                isMiss = true;
                break;
            default:
                isMiss = attackRoll + abilityModifier + weaponProficiencyBonus < target.ArmorClass;
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
            _logger.LogDebug($"{a.Name} is hiting");
            weaponPoints = DiceBox.Roll(a.EquipmentSockets?.Weapon.DiceType);
        }

        int totalPoints = weaponPoints +  abilityModifier;
        
        int hpBefore = target.HitPoints;
        target.HitPoints -= totalPoints;
        _logger.LogDebug($"{a.Name} use {a.EquipmentSockets?.Weapon.Name} hit roll({a.EquipmentSockets?.Weapon.DiceType}) {weaponPoints} + {abilityModifier} total {totalPoints}");
        _logger.LogDebug($"{target.Name} hit points: {target.HitPoints} = {hpBefore} - {totalPoints}");

        if (target.HitPoints <= 0)
        {
            target.HitPoints = 0;
            _logger.LogDebug($"{target.Name} Falling Unconscious");
        }
    }
}