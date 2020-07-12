using Net.Zxnn.Dnd.Core;

namespace Net.Zxnn.Dnd.Weapons
{
    public class Axe : Weapon
    {
        public Axe() : this("Axe", 0, 6) {

        }
        public Axe(string name, int min, int max) : base(name, min, max) {
        }
    }
}