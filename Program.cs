using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using BugReportBot.EventHandlers;
using BugReportBot.Configs;
using BugReportBot.Modules;

namespace BugReportBot
{
    class Program
    {
        private static Config? _config;
        private DiscordSocketClient? _client;
        private static readonly DiscordSocketConfig _socketConfig = new()
        {
            GatewayIntents = GatewayIntents.Guilds
        };

        public static Config Config => _config ??= GetConfig();
        public static System.Timers.Timer Interval = new System.Timers.Timer();
        public static int ?Id;

        public static Task Main(string[] args) => new Program().MainAsync();
        public async Task MainAsync()
        {
            using (ServiceProvider services = ConfigureServices())
            {
                _client = services.GetRequiredService<DiscordSocketClient>();

                await services.GetRequiredService<InteractionCreated>().InitializeAsync();
                await services.GetRequiredService<Ready>().InitializeAsync();
                await services.GetRequiredService<TimedEvent>().InitializeAsync();

                await _client.LoginAsync(TokenType.Bot, Config.Token);
                await _client.StartAsync();

                await Task.Delay(-1);
            };
        }
        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
             .AddSingleton(_socketConfig)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .AddSingleton<InteractionCreated>()
            .AddSingleton<Ready>()
            .AddSingleton<TimedEvent>()
            .BuildServiceProvider();
        }

        private static Config GetConfig()
        {
            string _path = Path.Combine(Environment.CurrentDirectory, @"Configs", "config.json");
            if (File.Exists(_path))
            {
                return JsonConvert.DeserializeObject<Config>(File.ReadAllText(_path))!;
            };

            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, @"Configs")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, @"Configs"));
            };

            File.WriteAllText(_path, JsonConvert.SerializeObject(Config.Default, Formatting.Indented));
            Log.Info("Config files created!");
            Environment.Exit(1);
            return Config.Default;
        }
    }
}