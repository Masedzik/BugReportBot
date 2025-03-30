using System.Text.RegularExpressions;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using BugReportBot.Modules;

namespace BugReportBot.Commands
{
    [Group("track", "Tracking group command.")]
    public class TrackCommand : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly DiscordSocketClient _client;
        public TrackCommand(DiscordSocketClient client)
        {
            _client = client;
        }

        [SlashCommand("start", "Starts tracking bugs.")]
        public async Task TrackStart(int id)
        {
            string response;

            if (Program.Interval.Enabled == true)
            {
                response = "Bug tracking is already active.";
                await RespondAsync(embed: await Embeds.CreateEmbed("/track start", response, Color.Red), ephemeral: true);
                Log.Info(response);
                return;
            };

            Program.Id = id;
            Program.Interval.Enabled = true;

            await _client.SetCustomStatusAsync("Tracking: ACTIVE");

            response = "Bug tracking activated.";
            await RespondAsync(embed: await Embeds.CreateEmbed("/track start", response, Color.Green), ephemeral:true);
            Log.Info(response);
            return;
        }

        [SlashCommand("stop", "Stops tracking bugs.")]
        public async Task TrackStop()
        {
            string response;

            if (Program.Interval.Enabled == false)
            {
                response = "Bug tracking is already inactive.";
                await RespondAsync(embed: await Embeds.CreateEmbed("/track stop", response, Color.Red), ephemeral: true);
                Log.Info(response);
                return;
            };

            Program.Id = null;
            Program.Interval.Enabled = false;

            await _client.SetCustomStatusAsync("Tracking: INACTIVE");

            response = "Bug tracking inactivated.";
            await RespondAsync(embed: await Embeds.CreateEmbed("/track stop", response, Color.Green), ephemeral: true);
            Log.Info(response);
            return;
        }
    }
}