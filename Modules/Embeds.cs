using System.Reflection;
using System.Text.RegularExpressions;

using Discord;

namespace BugReportBot.Modules
{
    public class Embeds
    {
        public static async Task<Embed> CreateEmbed(string command, string response, Color color)
        {
            Embed embed = new EmbedBuilder()
            {
                Title = command,
                Description = response,
                Color = color,
                Footer = new EmbedFooterBuilder() { Text = $"BugReportBot | {Regex.Replace(Assembly.GetExecutingAssembly().GetName().Version.ToString(), @".[0-9]$", "")} | - Mased • {DateTime.Now}" }
            }.Build();

            return embed;
        }
    }
}
