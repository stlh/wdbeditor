using System;
using System.Linq;

namespace Net.Zxnn.Dnd.Core
{
    public static class AbilityTools {
        public static int GetAbilityModifier(int score)
        {
            return (score - 10) / 2;
        }

        public static int[] StandardSetOfScores = new int[] {15, 14, 13, 12, 10, 8};
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
}