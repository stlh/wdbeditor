using System;

namespace Net.Zxnn.Dnd.Core
{
    public class AbilityTools {
        private static int[] abilityModifiers = new int[] {
            -5,
            -4, -4,
            -3, -3,
            -2, -2,
            -1, -1,
            0, 0,
            1, 1,
            2, 2,
            3, 3,
            4, 4,
            5, 5,
            6, 6,
            7, 7,
            8, 8,
            9, 9,
            10
        };

        public static int GetAbilityModifier(int score) {
           if (score < 1 || score > 30) {
               throw new ArgumentException("ability score must between 1 and 30.");
           }

           return abilityModifiers[score];
        }
    }
}