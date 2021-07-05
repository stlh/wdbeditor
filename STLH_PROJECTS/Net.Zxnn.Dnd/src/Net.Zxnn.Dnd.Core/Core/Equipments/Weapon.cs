namespace Net.Zxnn.Dnd.Core.Equipments
{
    public abstract class Weapon : Equipment
    {
        public string DiceType { get; set; }

        public string Name { get; set; }

        public Weapon(string name, string diceType) {
            this.Name = name;
            this.DiceType = diceType;
        }
    }
}