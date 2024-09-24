namespace BugReportBot.Modules
{
    public class Bugs
    {
        public static void save(string id, string message)
        {
            if (Program.Config.Logging)
            {
                string current = $"Bug-{id}.html";
                string path = Path.Combine(Environment.CurrentDirectory, @"Bugs", current);

                if (!Directory.Exists(path.Replace(current, "")))
                {
                    Directory.CreateDirectory(path.Replace(current, ""));
                };

                if (File.Exists(path))
                {
                    return;
                };

                File.WriteAllText(path, message);
            };
        }
    }
}
