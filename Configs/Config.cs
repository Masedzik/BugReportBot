namespace BugReportBot.Configs
{
    public class Config
    {
        public string Token { get; set; }
        public ulong Guild { get; set; }
        public ulong Channel { get; set; }

        public Dictionary<string, List<string>> Permissions { get; set; } = new Dictionary<string, List<string>>();

        public string Link { get; set; }
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

            Link = "https://git.scpslgame.com/northwood-qa/scpsl-bug-reporting/-/issues/",
            Min = 10,
            Max = 20,

            Logging = true
        };
    }
}