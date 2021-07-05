using Net.Zxnn.Dnd.Core;
using Net.Zxnn.Dnd.Core.Equipments.Weapons;

namespace Net.Zxnn.Dnd.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Character character001 = new CharacterBuilder()
                .WithName("Skoll-001")
                .Build();

            Character character002 = new CharacterBuilder()
                .WithName("Skoll-002")
                .Build();

            character001.EquipmentSockets.Weapon = new Battleaxe();

            character001.move();
            character001.attack(character002);
        }
    }
}
