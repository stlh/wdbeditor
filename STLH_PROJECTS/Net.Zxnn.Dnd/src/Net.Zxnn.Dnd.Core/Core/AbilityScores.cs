using System;
using System.Collections.Generic;
using System.Linq;

namespace Net.Zxnn.Dnd.Core {
    public class AbilityScores {
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

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
    }
}