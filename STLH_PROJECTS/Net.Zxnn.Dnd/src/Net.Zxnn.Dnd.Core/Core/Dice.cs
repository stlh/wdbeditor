using System;

namespace Net.Zxnn.Dnd.Core
{
    
    public class Dice
    {
        public int Faces { get; init; }

        public Dice(int faces = 6)
        {
            this.Faces = faces;
        }
                                                                                       
        public int Roll()
        {
            Random random = new Random();
            return random.Next(1, this.Faces + 1);
        }

        public int Max
        { 
            get
            {
                return this.Faces;
            }
        }

        public static readonly Dice d20 = new Dice(20);
        public static readonly Dice d12 = new Dice(12);
        public static readonly Dice d10 = new Dice(10);
        public static readonly Dice d8 = new Dice(8);
        public static readonly Dice d6 = new Dice();
    }
}