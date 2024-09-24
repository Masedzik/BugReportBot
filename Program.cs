using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using BugReportBot.EventHandlers;
using BugReportBot.Configs;
using BugReportBot.Modules;

namespace BugReportBot
{
    class Program
    {
        public static readonly ConfigReader Config = new ConfigReader();
        public static System.Timers.Timer Interval = new System.Timers.Timer();
        public static int ?Id;

        private DiscordSocketClient? _client;
        private static readonly DiscordSocketConfig _socketConfig = new()
        {
            GatewayIntents = GatewayIntents.Guilds,
            AlwaysDownloadUsers = true,
        };

        public static Task Main(string[] args) => new Program().MainAsync();
        public async Task MainAsync()
        {
            if (await Config.ReadConfigs())
            {
                using (ServiceProvider services = ConfigureServices())
                {
                    _client = services.GetRequiredService<DiscordSocketClient>();

                    await services.GetRequiredService<TimedEvent>().InitializeAsync();
                    await services.GetRequiredService<InteractionCreated>().InitializeAsync();
                    await services.GetRequiredService<Ready>().InitializeAsync();

                    await _client.LoginAsync(TokenType.Bot, Config.Token);
                    await _client.StartAsync();

                    await Task.Delay(-1);
                };
            }
            else
            {
                Logs.Log("Config files created!");
            };
        }
        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
             .AddSingleton(_socketConfig)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .AddSingleton<TimedEvent>()
            .AddSingleton<InteractionCreated>()
            .AddSingleton<Ready>()
            .BuildServiceProvider();
        }
    }
}