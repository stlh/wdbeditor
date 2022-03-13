using System;

using Net.Zxnn.Dnd.Core;
using Net.Zxnn.Dnd.Core.Class;
using Net.Zxnn.Dnd.Core.Race;
using Xunit;
using Xunit.Abstractions;

namespace Net.Zxnn.Dnd.Core.Tests;
public class CharacterSheetTest
{
    private readonly ITestOutputHelper output;

    public CharacterSheetTest(ITestOutputHelper outputHelper)
    {
        this.output = outputHelper;
    }
    
    [Fact]
    public void TestPrintCharacter()
    {
        Character c001 = new CharacterBuilder()
            .WithRace(DndRace.Human)
            .WithClass(DndClass.Fighter)
            .WithAbilities(AbilityTools.GetStandardSetOfScores())
            .WithName("Human-Fighter")
            .Build();
        
        String sheet = new CharacterSheet(c001).PrintCharacter();

        output.WriteLine(sheet);
    }
}