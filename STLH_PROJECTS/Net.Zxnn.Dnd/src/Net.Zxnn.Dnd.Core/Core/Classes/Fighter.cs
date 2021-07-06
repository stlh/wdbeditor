using System;

namespace Net.Zxnn.Dnd.Core.Classes
{
    public class Fighter : Class {
        public override string HitDice {
            get {
                return "d10";
            }
        }
    } 
}