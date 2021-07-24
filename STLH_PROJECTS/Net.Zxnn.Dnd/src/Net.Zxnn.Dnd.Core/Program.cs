using Net.Zxnn.Dnd.Core;
using Net.Zxnn.Dnd.Core.Class;
using Net.Zxnn.Dnd.Core.Race;
using Net.Zxnn.Dnd.Core.Equipments.Weapons;

namespace Net.Zxnn.Dnd.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Character c001 = new CharacterBuilder()
                .WithRace(DndRace.Human)
                .WithClass(DndClass.Fighter)
                .WithName("Skoll-001")
                .Build();

            Character c002 = new CharacterBuilder()
                .WithRace(DndRace.Human)
                .WithClass(DndClass.Fighter)
                .WithName("Skoll-002")
                .Build();

            c001.EquipmentSockets.Weapon = new Battleaxe();
            c002.EquipmentSockets.Weapon = new Battleaxe();

            c001.move();
            c002.move();

            while (c001.HitPoints > 0 && c002.HitPoints > 0) {
                c001.attack(c002);
                if (c002.HitPoints > 0) {
                    c002.attack(c001);
                }
            }
        }
    }
}
