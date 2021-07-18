using System;

namespace Net.Zxnn.Dnd.Core
{
    public static class AbilityTools {
        public static int GetAbilityModifier(int score)
        {
            return Convert.ToInt32(Math.Floor((score - 10) / 2d));
        }
    }
}