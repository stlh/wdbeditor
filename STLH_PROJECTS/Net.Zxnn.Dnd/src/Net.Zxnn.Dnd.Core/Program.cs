using System;
using System.CommandLine;
using System.CommandLine.Invocation;

using Net.Zxnn.Dnd.Core;
using Net.Zxnn.Dnd.Core.Class;
using Net.Zxnn.Dnd.Core.Race;
using Net.Zxnn.Dnd.Core.Equipments.Weapons;

namespace Net.Zxnn.Dnd.Core
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var rootCommand = new RootCommand
            {
                new Option<int>(
                    "--round-count",
                    getDefaultValue: () => 1000,
                    description: "how many rounds to be fight"
                )
            };

            rootCommand.Description = "DnD test application";

            rootCommand.Handler = CommandHandler.Create<int>((int roundCount) =>
            {
                Console.WriteLine($"round count: {roundCount}");

                Character c001 = new CharacterBuilder()
                    .WithRace(DndRace.Human)
                    .WithClass(DndClass.Fighter)
                    .WithAbilities(AbilityTools.GetRandomStandardSetOfScores())
                    .WithName("Human-Fighter")
                    .Build();

                Character c002 = new CharacterBuilder()
                    .WithRace(DndRace.Dwarf)
                    .WithClass(DndClass.Barbarian)
                    .WithAbilities(AbilityTools.GetRandomStandardSetOfScores())
                    .WithName("Dwarf-Barbarian")
                    .Build();

                c001.EquipmentSockets.Weapon = new Battleaxe();
                c002.EquipmentSockets.Weapon = new Battleaxe();
                //c002.EquipmentSockets.Shield = new Shield();

                c001.move();
                c002.move();

                int c001Win = 0;
                int c002Win = 0;
                for (int i = 0; i < roundCount; ++i)
                {
                    while (c001.HitPoints > 0 && c002.HitPoints > 0)
                    {
                        if (i % 2 == 0) {
                            c001.attack(c002);
                            if (c002.HitPoints > 0)
                            {
                                c002.attack(c001);
                            }
                        }
                        else
                        {
                            c002.attack(c001);
                            if (c001.HitPoints > 0)
                            {
                                c001.attack(c002);
                            }
                        }
                    }

                    if (c001.HitPoints > 0)
                    {
                        c001Win += 1;
                    }
                    else if (c002.HitPoints > 0)
                    {
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

                return 0;
            });

            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
