namespace Net.Zxnn.Dnd.Core.Race
{
    public readonly struct AbilityScoreIncrease
    {
        public AbilityScoreIncrease(int strength = 0, int dexterity = 0, int constitution = 0, int intelligence = 0, int wisdom = 0, int charisma = 0)
        {
            StrongthIncrease = strength;
            DexterityIncrease = dexterity;
            ConstitutionIncrease = constitution;
            IntelligenceIncrease = intelligence;
            WisdomIncrease = wisdom;
            CharismaIncrease = charisma;
        }
        public int StrongthIncrease { get; init; }
        public int DexterityIncrease { get; init; }
        public int ConstitutionIncrease { get; init; }
        public int IntelligenceIncrease { get; init; }
        public int WisdomIncrease { get; init; }
        public int CharismaIncrease { get; init; }
    }
}