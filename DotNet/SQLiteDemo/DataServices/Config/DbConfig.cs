namespace Nadam.DataServices.Config
{
    public abstract class DbConfig
    {
        public string DbName { get; set; }
        public abstract SupportedDbEngines DbEngine { get; }
    }
}
