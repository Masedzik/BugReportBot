using Newtonsoft.Json;
using System.Text;

namespace BugReportBot.Configs
{
    public class ConfigReader
    {
        public string Token = "";
        public ulong Guild = 0;
        public ulong Channel = 0;

        public Dictionary<string, List<string>> Permissions = new Dictionary<string, List<string>>();

        public string Link = "";
        public short Min = 0;
        public short Max = 0;

        public bool Logging = true;

        public async Task<bool> ReadConfigs()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Configs", "config.json");

            if (!Directory.Exists(path.Replace("config.json", "")))
            {
                Directory.CreateDirectory(path.Replace("config.json", ""));
            };

            if (!File.Exists(path)) 
            {
                ConfigStruct config = new ConfigStruct() { token = "", guild = 0, channel = 0,  permissions = new Dictionary<string, List<string>>(1) { ["discordID"] = new List<string>(1) {"command.subcommand"} }, link = "link", min = 1, max = 5, logging = true};
                string content = JsonConvert.SerializeObject(config, new JsonSerializerSettings(){Formatting = Formatting.Indented});

                File.WriteAllText(path, content);
                return false; 
            };
           
            using (StreamReader sr = new StreamReader(path , new UTF8Encoding(false)))
            {
                string json = await sr.ReadToEndAsync();
                ConfigStruct configStruct = JsonConvert.DeserializeObject<ConfigStruct>(json);

                this.Token = configStruct.token;
                this.Guild = configStruct.guild;
                this.Channel = configStruct.channel;
                this.Permissions = configStruct.permissions;
                this.Link = configStruct.link;
                this.Min = configStruct.min;
                this.Max = configStruct.max;
                this.Logging = configStruct.logging;
                return true;
            }
        }
    }

   internal struct ConfigStruct
   {
        public string token;
        public ulong guild;
        public ulong channel;

        public Dictionary<string,List<string>> permissions;

        public string link;

        public short min;
        public short max;

        public bool logging;
    }
}
