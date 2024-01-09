namespace Nadam.DataServices.Config
{
    public class SqliteConfig : DbConfig
    {
        public string DbFileName { get; set; }
        public override SupportedDbEngines DbEngine { get => SupportedDbEngines.Sqlite; }
    }
}
