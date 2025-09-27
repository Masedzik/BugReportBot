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
            float rand = random.Next((Program.Config.Min < 60 ? 60 : Program.Config.Min), Program.Config.Max);
            Program.Interval.Interval = rand * 1000;

            Log.Info($"Next check in {Math.Floor(rand / 60)}:{rand % 60}");

            using (WebClient client = new WebClient())
            {
                try
                {
                    ITextChannel channel = _client.GetChannel(Program.Config.Channel) as ITextChannel;

                    string content = await client.DownloadStringTaskAsync($"{Program.Config.TrackLink}{Program.Id}");
                    if (Regex.Match(content, @"<pre>{""message"":""404 Not found""}</pre>").Success) return;

                    await channel.SendMessageAsync(text: $"{Program.Config.ShortLink}{Program.Id}");
                    Bug.Save(Program.Id.ToString(), content);

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