namespace Net.Zxnn.Dnd.Core.Race
{
    public readonly struct AbilityScoreIncrease
    {
        public AbilityScoreIncrease(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
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