namespace SQLiteDemo.Config
{
    public class SqliteConfig : DbConfig
    {
        public string DbFileName { get; set; }
        public StorageProvider BackupProvider { get; set; }
        public override SupportedDbEngines DbEngine { get => SupportedDbEngines.Sqlite; }

        private void HydrateDb()
        {
            var backup = BackupProvider.GetFile("<key to db backup>");
        }
    }
}
