using System;
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
                .WithRace(DndRace.Dwarf)
                .WithClass(DndClass.Fighter)
                .WithName("Skoll-002")
                .Build();

            c001.EquipmentSockets.Weapon = new Battleaxe();
            c002.EquipmentSockets.Weapon = new Battleaxe();
            c002.EquipmentSockets.Shield = new Shield();

            c001.move();
            c002.move();

            int c001Win = 0;
            int c002Win = 0;
            for (int i = 0; i < 10000; ++i)
            {
                while (c001.HitPoints > 0 && c002.HitPoints > 0) {
                    c001.attack(c002);
                    if (c002.HitPoints > 0) {
                        c002.attack(c001);
                    }
                }

                if (c001.HitPoints > 0) {
                    c001Win += 1;
                }
                else if (c002.HitPoints > 0) {
                    c002Win += 1;
                }
                else
                {
                    Console.WriteLine("who win?");
                }

                // reset hit points
                c001.HitPoints = c001.HitPointMax;
                c002.HitPoints = c002.HitPointMax;
            }

            Console.WriteLine($"c001 win: {c001Win}, c002 win: {c002Win}");
        }
    }
}
