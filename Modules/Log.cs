namespace BugReportBot.Modules
{
    public class Log
    {
        private static string current = $"Log-{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
        private static string path = Path.Combine(Environment.CurrentDirectory, @"Logs", current);
        private static void Check()
        {
            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, @"Logs")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, @"Logs"));
            };

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            };
        }

        public static void Info(string message)
        {
            Check();
            File.AppendAllText(path, $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}][INFO] {message}" + Environment.NewLine);
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}][INFO] {message}");
        }

        public static void Error(string message)
        {
            Check();
            File.AppendAllText(path, $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}][ERROR] {message}" + Environment.NewLine);
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}][ERROR] {message}");
        }
    }
}
