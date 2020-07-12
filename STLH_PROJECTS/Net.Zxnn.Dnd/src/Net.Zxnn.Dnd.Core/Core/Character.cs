using System;

namespace Net.Zxnn.Dnd.Core
{
    public class Character : ICharacter
    {
        public Character(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }

        public Weapon Weapon {get; set; }

        public void attack()
        {
            Console.WriteLine($"{Name} is attacking by {Weapon.Name} {DiceBox.Roll(this.Weapon == null ? this.Weapon.DiceType : "1d3")}");
        }

        public void move()
        {
            Console.WriteLine($"{Name} is moving.");
        }
    }
}