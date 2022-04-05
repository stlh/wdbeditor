using System;
using System.Linq;

namespace Net.Zxnn.Dnd.Core;
public static class AbilityTools
{
    public static int GetAbilityModifier(int score)
    {
        // return (score - 10) / 2;
        return (score - 10) >> 1;
    }

    public static int GetProficiencienBonus(int level)
    {
        return (level -1) / 4 + 2;
    }

    public static int[] StandardSetOfScores = new int[] {15, 14, 13, 12, 10, 8};

    public static AbilityScores GetStandardSetOfScores()
    {
        return new AbilityScores()
        {
            Strength = StandardSetOfScores[0],
            Dexterity = StandardSetOfScores[1],
            Constitution = StandardSetOfScores[2],
            Intelligence = StandardSetOfScores[3],
            Wisdom = StandardSetOfScores[4],
            Charisma = StandardSetOfScores[5]
        };
    }

    public static AbilityScores GetRandomStandardSetOfScores()
    {
        int[] tempArray = StandardSetOfScores.OrderBy(_ => Guid.NewGuid()).ToArray();
        
        return new AbilityScores()
        {
            Strength = tempArray[0],
            Dexterity = tempArray[1],
            Constitution = tempArray[2],
            Intelligence = tempArray[3],
            Wisdom = tempArray[4],
            Charisma = tempArray[5]
        };
    }
}