namespace SQLiteDemo.Config
{
    public abstract class DbConfig
    {
        public abstract SupportedDbEngines DbEngine { get; }
    }
}
