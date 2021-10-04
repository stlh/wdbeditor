using System;

namespace Net.Zxnn.Dnd.Core
{
    
    public class Dice
    {
        public int Faces { get; }

        public Dice(int faces = 6)
        {
            this.Faces = faces;
        }

        public int Roll()
        {
            Random random = new Random();
            return random.Next(1, this.Faces + 1);
        }

        private static Dice d20 = new Dice(20);
        public static Dice D20 {
            get {
                return d20;
            }
        }
    }
}