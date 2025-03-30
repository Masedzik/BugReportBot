using System.Reflection;

using Discord.Interactions;
using Discord.WebSocket;

using BugReportBot.Modules;

namespace BugReportBot.EventHandlers
{
    public class Ready
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;
        private readonly IServiceProvider _services;

        public Ready(DiscordSocketClient client, InteractionService commands, IServiceProvider services)
        {
            _client = client;
            _commands = commands;
            _services = services;
        }

        public async Task InitializeAsync()
        {
            _client.Ready += OnReady;
        }

        private async Task OnReady()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            await _commands.RegisterCommandsToGuildAsync(Program.Config.Guild);

            await _client.SetCustomStatusAsync("Tracking bugs :3");
            Log.Info($"Welcome to BugReportingBot v2.0.0 !");
            Log.Info($"Connected as [{_client.CurrentUser}]");
        }
    }
}