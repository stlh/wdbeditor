using System;

using Microsoft.Extensions.Logging.Abstractions;

using Net.Zxnn.Dnd.Core.Class;
using Net.Zxnn.Dnd.Core.Equipments.Weapons;
using Net.Zxnn.Dnd.Core.Race;

using Xunit;
using Xunit.Abstractions;

namespace Net.Zxnn.Dnd.Core.Tests;
public class CombatServiceTest
{
    private readonly ITestOutputHelper _output;

    private CombatService combatService;

    public CombatServiceTest(ITestOutputHelper outputHelper)
    {
        this._output = outputHelper;

        combatService = new CombatService(new NullLogger<CombatService>());
    }

    [Fact]
    public void AttackTest()
    {
        Character c001 = new CharacterBuilder()
                    .WithRace(DndRace.Human)
                    .WithClass(DndClass.Fighter)
                    .WithAbilities(AbilityTools.GetStandardSetOfScores())
                    .WithName("Human-Fighter")
                    .Build();

        Character c002 = new CharacterBuilder()
                    .WithRace(DndRace.Dwarf)
                    .WithClass(DndClass.Barbarian)
                    .WithAbilities(AbilityTools.GetStandardSetOfScores())
                    .WithName("Dwarf-Barbarian")
                    .Build();

        c001.EquipmentSockets.Weapon = new Axe();
        c002.EquipmentSockets.Weapon = new BattleAxe();

        combatService.attack(c001, c002);

        Assert.NotEqual(c002.HitPoints, c002.HitPointMax);
    }
}