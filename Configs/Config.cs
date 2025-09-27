namespace BugReportBot.Configs
{
    public class Config
    {
        public string Token { get; set; }
        public ulong Guild { get; set; }
        public ulong Channel { get; set; }

        public Dictionary<string, List<string>> Permissions { get; set; } = new Dictionary<string, List<string>>();

        public string TrackLink { get; set; }
        public string ShortLink { get; set; }
        public short Min {  get; set; }
        public short Max {  get; set; }

        public bool Logging {  get; set; }

        public static Config Default => new()
        {
            Token = "Token",
            Guild = 0,
            Channel = 0,

            Permissions = new Dictionary<string, List<string>>() 
            {
                {
                    "DiscordId",
                    new List<string>
                    {
                        {
                            "*.*"
                        }
                    }
                }
            },

            TrackLink = "https://git.scpslgame.com/api/v4/projects/75/issues/",
            ShortLink = "https://git.scpslgame.com/northwood-qa/scpsl-bug-reporting/-/issues/",
            Min = 60,
            Max = 60,

            Logging = true
        };
    }
}