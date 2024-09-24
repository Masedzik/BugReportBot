namespace BugReportBot.Modules
{
    public class Logs
    {
        public static void Log(string message) 
        {
            if (Program.Config.Logging) 
            {
                string current = $"Log-{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
                string path = Path.Combine(Environment.CurrentDirectory, @"Logs", current);

                if (!Directory.Exists(path.Replace(current, "")))
                {
                    Directory.CreateDirectory(path.Replace(current, ""));
                };

                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                };

                File.AppendAllText(path, $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {message}" + Environment.NewLine);
                Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {message}");
            };
        }
    }
}
