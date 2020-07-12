namespace Net.Zxnn.Dnd.Core
{
    public abstract class Weapon : Equipment
    {
        public string Name { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public Weapon(string name, int min, int max) {
            this.Name = name;
            this.Min = min;
            this.Max = max;
        }
    }
}