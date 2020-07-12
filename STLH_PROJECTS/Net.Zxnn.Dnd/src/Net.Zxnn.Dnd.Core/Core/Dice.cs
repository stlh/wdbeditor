using System;

namespace Net.Zxnn.Dnd.Core
{
    
    public class Dice
    {
        private static readonly Random random = new Random();
        public int Faces { get; set; }

        public Dice(int faces = 6)
        {
            this.Faces = faces;
        }

        public int Roll()
        {
            return random.Next(1, this.Faces + 1);
        }
    }
}