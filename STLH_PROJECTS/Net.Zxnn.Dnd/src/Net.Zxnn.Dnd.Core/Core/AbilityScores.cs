using System;

namespace Net.Zxnn.Dnd.Core {
    public class AbilityScores {
        public AbilityScores(int strength = 0, int dexterity = 0, int constitution = 0, int intelligence = 0, int wisdom = 0, int charisma = 0)
        {
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Charisma = charisma;
        }

        public int Strength { get; init; }
        public int Dexterity { get; init; }
        public int Constitution { get; init; }
        public int Intelligence { get; init; }
        public int Wisdom { get; init; }
        public int Charisma { get; init; }

        public static AbilityScores operator +(AbilityScores left, AbilityScores right) =>
            new AbilityScores()
            {
                Strength = left.Strength + right.Strength,
                Dexterity = left.Dexterity + right.Dexterity,
                Constitution = left.Constitution + right.Constitution,
                Intelligence = left.Intelligence + right.Intelligence,
                Wisdom = left.Wisdom + right.Wisdom,
                Charisma = left.Charisma + right.Charisma,
            };
    }
}