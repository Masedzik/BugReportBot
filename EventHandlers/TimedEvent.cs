using Discord;
using Discord.WebSocket;
using System.Net;
using BugReportBot.Modules;
using System.Text.RegularExpressions;

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

        private async void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Random random = new Random();
            float rand = random.Next(Program.Config.Min * 60, Program.Config.Max * 60);
            Program.Interval.Interval = rand * 1000;

            Logs.Log($"Next check in {Math.Floor(rand / 60)}:{rand % 60}");

            using (WebClient client = new WebClient())
            {
                try
                {
                    if (Program.Id != null)
                    {
                        string content = await client.DownloadStringTaskAsync($"{Program.Config.Link}{Program.Id}");

                        IMessageChannel channel = _client.GetChannel(Program.Config.Channel) as IMessageChannel;
                        await channel.SendMessageAsync(text: $"{Program.Config.Link}{Program.Id}");

                        string title = Regex.Match(content, @"<div class=""title-container"">\s<h1(.)*</h1>\s</div>").Value;
                        string body = Regex.Match(content, @"<div class=""description"">\s<div class=""md"">(.)*\s</div>").Value;

                        if (title != null && body != null) 
                        {
                            Bugs.save(Program.Id.ToString(),$"{title}\n\n{body}");
                        };

                        Program.Id++;
                    };
                }
                catch (Exception ex)
                {
                    Logs.Log(ex.Message);
                };
            };
        }
    }
}