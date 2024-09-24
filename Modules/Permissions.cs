namespace BugReportBot.Modules
{
    public class Permissions
    {
        public static bool CheckPermissions(string permission, string user)
        {
            bool response = false;
            string[] commandPerm = permission.Split('.');

            Program.Config.Permissions.TryGetValue(user, out List<string>? Value);
            if (Value == null)
            {
                return false;
            };

            foreach (string perm in Value)
            {
                string[] userPerm = perm.Split(".");
                bool result = true;

                for (int i = 0; i < commandPerm.Length; i++)
                {
                    if (userPerm[i] != commandPerm[i] && userPerm[i] != "*")
                    {
                        result = false;
                    };
                };

                if (result == true)
                {
                    response = true;
                }
            };

            return response;
        }
    }
}
