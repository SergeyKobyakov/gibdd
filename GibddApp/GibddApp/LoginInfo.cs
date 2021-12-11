namespace GibddApp
{
    internal static class LoginInfo
    {
        public static string Login { get; set; }
        public static string Password { get; set; }
        public static Dictionary<string, HashSet<string>> Privileges { get; set; } = new();
        public static bool IsSysDba { get; set; }

        public static bool CanSelect(string tableName)
        {
            return Privileges.ContainsKey(tableName) &&
                Privileges[tableName].Contains("S");
        }

        public static bool CanUpdate(string tableName)
        {
            return Privileges.ContainsKey(tableName) &&
                Privileges[tableName].Contains("U");
        }

        public static bool CanInsert(string tableName)
        {
            return Privileges.ContainsKey(tableName) &&
                Privileges[tableName].Contains("I");
        }
        
        public static bool CanDelete(string tableName)
        {
            return Privileges.ContainsKey(tableName) &&
                Privileges[tableName].Contains("D");
        }
    }
}
