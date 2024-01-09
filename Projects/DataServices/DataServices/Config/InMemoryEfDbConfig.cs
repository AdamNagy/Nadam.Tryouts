namespace Nadam.DataServices.Config
{
    public class InMemoryEfDbConfig : DbConfig
    {
        public override SupportedDbEngines DbEngine => SupportedDbEngines.InMemory;

        public InMemoryEfDbConfig(string dbName)
        {
            DbName = dbName;
        }
    }
}
