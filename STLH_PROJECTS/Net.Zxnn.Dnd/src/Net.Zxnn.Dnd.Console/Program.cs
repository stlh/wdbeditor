using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Net.Zxnn.Dnd.Core;
using Net.Zxnn.Dnd.Scenes;

namespace Net.Zxnn.Dnd;

class Program
{
    static int Main(string[] args)
    {
        var rootCommand = ConfigureCommand();
        
        return rootCommand.InvokeAsync(args).Result;
    }

    private static RootCommand ConfigureCommand()
    {
        var rootCommand = new RootCommand();

        var countOption = new Option<int>(
            "--round-count",
            getDefaultValue: () => 1000,
            description: "how many rounds to be fight"
        );

        var logLevelOption = new Option<string>(
            "--log-level",
            getDefaultValue: () => "Info",
            description: "log level"
        );

        rootCommand.AddOption(countOption);
        rootCommand.AddOption(logLevelOption);

        rootCommand.Description = "DnD test application";

        rootCommand.SetHandler((roundCount, logLevel) =>
        {
            var services = new ServiceCollection();

            ConfigureServices(services, logLevel);

            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            Scene001? scene001 = serviceProvider.GetService<Scene001>();

            scene001?.Run(roundCount);
        }, countOption, logLevelOption);

        return rootCommand;
    }

    private static void ConfigureServices(ServiceCollection services, string logLevel)
    {
        services.AddLogging(configure =>
                configure.AddFilter("Net.Zxnn.Dnd", getLogLevel(logLevel))
                    .AddConsole())
            .AddTransient<Scene001>()
            .AddTransient<CombatService>();
    }

    private static LogLevel getLogLevel(string logLevel)
    {
        switch(logLevel)
        {
            case "debug":
                return LogLevel.Debug;
            case "info":
                return LogLevel.Information;
            default:
                return LogLevel.Information;
        }
    }
}