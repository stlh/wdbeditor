using Net.Zxnn.Dnd.Core;
using Net.Zxnn.Dnd.Core.Class;
using Net.Zxnn.Dnd.Core.Equipments.Weapons;
using Net.Zxnn.Dnd.Core.Race;
using Microsoft.Extensions.Logging;

namespace Net.Zxnn.Dnd.Core
{
    public class Scene
    {
        private readonly ILogger _logger;
        private readonly CombatService _combatService;

        public Scene(ILogger<Scene> logger, CombatService combatService)
        {
            _logger = logger;
            _combatService = combatService;
        }

        public void Run(int roundCount)
        {
            _logger.LogInformation($"round count: {roundCount}");

            int c001Win = 0;
            int c002Win = 0;
            for (int i = 0; i < roundCount; ++i)
            {
                Character c001 = new CharacterBuilder()
                    .WithRace(DndRace.Human)
                    .WithClass(DndClass.Fighter)
                    .WithAbilities(AbilityTools.GetRandomStandardSetOfScores())
                    .WithName("Human-Fighter")
                    .Build();

                Character c002 = new CharacterBuilder()
                    .WithRace(DndRace.Dwarf)
                    .WithClass(DndClass.Barbarian)
                    .WithAbilities(AbilityTools.GetRandomStandardSetOfScores())
                    .WithName("Dwarf-Barbarian")
                    .Build();

                c001.EquipmentSockets.Weapon = new BattleAxe();
                c002.EquipmentSockets.Weapon = new BattleAxe();

                while (c001.HitPoints > 0 && c002.HitPoints > 0)
                {
                    if (c001.AbilityScores.Dexterity >= c002.AbilityScores.Dexterity)
                    {
                        _combatService.attack(c001, c002);
                        if (c002.HitPoints > 0)
                        {
                            _combatService.attack(c002, c001);
                        }
                    }
                    else
                    {
                        _combatService.attack(c002, c001);
                        if (c001.HitPoints > 0)
                        {
                            _combatService.attack(c001, c002);
                        }
                    }
                }

                if (c001.HitPoints > 0)
                {
                    c001Win += 1;
                }
                else if (c002.HitPoints > 0)
                {
                    c002Win += 1;
                }
                else
                {
                    _logger.LogDebug("who win?");
                }

                // reset hit points
                c001.HitPoints = c001.HitPointMax;
                c002.HitPoints = c002.HitPointMax;
            }

            _logger.LogInformation($"c001 win: {c001Win}, c002 win: {c002Win}");
        }
    }
}