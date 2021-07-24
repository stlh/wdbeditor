using System;
using Net.Zxnn.Dnd.Core.Class.Classes;

namespace Net.Zxnn.Dnd.Core.Class
{
    public abstract class DndClass
    {
        public static readonly DndClass Fighter = new Fighter();
        public abstract string HitDice { get; }
    }
}