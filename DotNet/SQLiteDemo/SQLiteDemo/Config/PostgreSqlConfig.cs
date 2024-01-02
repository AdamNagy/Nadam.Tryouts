namespace SQLiteDemo.Config
{
    public class PostgreSQLConfig : DbConfig
    {
        public override SupportedDbEngines DbEngine { get => SupportedDbEngines.Postgre; }

        public string Host { get; private set; }
        // public string DbName { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public PostgreSQLConfig(string host, string dbName, string userName, string password)
        {
            Host = host;
            DbName = dbName;
            Username = userName;
            Password = password;
        }
    }
}
