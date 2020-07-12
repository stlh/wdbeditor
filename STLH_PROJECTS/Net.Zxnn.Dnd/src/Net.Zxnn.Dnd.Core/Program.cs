using Net.Zxnn.Dnd.Core;
using Net.Zxnn.Dnd.Weapons;

namespace Net.Zxnn.Dnd.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Character character001 = new Character("Skoll");

            character001.Weapon = new Axe();

            character001.move();
            character001.attack();
        }
    }
}
