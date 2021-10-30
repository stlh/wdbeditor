using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Net.Zxnn.Dnd.Core;

namespace Net.Zxnn.Dnd
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

            rootCommand.Handler = CommandHandler.Create<int>((int roundCount) => {
                var services = new ServiceCollection();

                ConfigureServices(services);

                using (ServiceProvider serviceProvider = services.BuildServiceProvider())
                {
                    Scene scene = serviceProvider.GetService<Scene>();
                    
                    scene.Run(roundCount);
                }
            });

            return rootCommand.InvokeAsync(args).Result;
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(configure => 
                    configure.AddFilter("Net.Zxnn.Dnd", LogLevel.Information)
                        .AddConsole())
                .AddTransient<Scene>()
                .AddTransient<CombatService>();
        }
    }
}
