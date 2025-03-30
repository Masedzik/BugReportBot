using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using BugReportBot.Modules;

namespace BugReportBot.EventHandlers
{
    public class InteractionCreated
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;
        private readonly IServiceProvider _services;

        public InteractionCreated(DiscordSocketClient client, InteractionService commands, IServiceProvider services)
        {
            _client = client;
            _commands = commands;
            _services = services;
        }

        public async Task InitializeAsync()
        {
            _client.InteractionCreated += OnInteractionCreated;
        }

        private async Task OnInteractionCreated(SocketInteraction arg)
        {
            try
            {
                if (arg.Type == InteractionType.ApplicationCommand)
                {
                    SocketSlashCommand command = ((SocketSlashCommand)arg);

                    if (Permissions.CheckPermissions($"{command.CommandName}.{command.Data.Options.ToArray()[0].Name}", command.User.Id.ToString()) != true)
                    {
                        await arg.RespondAsync(embed: await Embeds.CreateEmbed($"/{command.CommandName} {command.Data.Options.ToArray()[0].Name}", "You don't have permissions to use this command!", Color.Red));
                        return;
                    };
                };

                var ctx = new SocketInteractionContext(_client, arg);
                await _commands.ExecuteCommandAsync(ctx, _services);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            };
        }
    }
}