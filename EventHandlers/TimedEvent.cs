using System.Net;
using System.Text.RegularExpressions;

using Discord;
using Discord.WebSocket;

using BugReportBot.Modules;

namespace BugReportBot.EventHandlers
{
    public class TimedEvent
    {
        private readonly DiscordSocketClient _client;
        public TimedEvent(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task InitializeAsync()
        {
            Program.Interval.Elapsed += OnTimedEvent; 
        }

        private async void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs ev)
        {
            Random random = new Random();
            float rand = random.Next(Program.Config.Min * 60, Program.Config.Max * 60);
            Program.Interval.Interval = rand * 1000;

            Log.Info($"Next check in {Math.Floor(rand / 60)}:{rand % 60}");

            using (WebClient client = new WebClient())
            {
                try
                {
                    ITextChannel channel = _client.GetChannel(Program.Config.Channel) as ITextChannel;
                    channel.ModifyAsync(x => x.Topic = $"<t:{(int)DateTimeOffset.Now.ToUnixTimeSeconds() + (int)rand}:R>");

                    string content = await client.DownloadStringTaskAsync($"{Program.Config.Link}{Program.Id}");

                    string title = Regex.Match(content, @"<div class=""title-container"">\s<h1(.)*</h1>\s</div>").Value;
                    string body = Regex.Match(content, @"<div class=""description"">\s<div class=""md"">(.)*\s</div>").Value;

                    if (title != null && body != null) 
                    {
                        await channel.SendMessageAsync(text: $"{Program.Config.Link}{Program.Id}");
                        Bug.Save(Program.Id.ToString(),$"{title}\n\n{body}");
                    };

                    Program.Id++;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                };
            };
        }
    }
}