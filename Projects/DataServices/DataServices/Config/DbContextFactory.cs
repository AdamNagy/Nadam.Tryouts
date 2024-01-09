using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Nadam.DataServices.Config
{
    public interface IStorage
    {
        bool GetObject(string key, out byte[] data);
        void SaveObject(string key, byte[] data, bool overwrite);
    }

    public interface ILocalStorage : IStorage
    {
        public string Root { get; }
    }

    public class DbContextFactory<T> where T : DbContext
    {
        private readonly ILocalStorage _localStorage;
        private readonly IStorage _cloudStorage;

        public DbContextFactory(ILocalStorage localStorage, IStorage cloudStorage)
        {
            _localStorage = localStorage;
            _cloudStorage = cloudStorage;
        }

        public T CreateContext(DbConfig config)
        {
            var dbContextType = typeof(T);
            var ctor = dbContextType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, new[] { typeof(DbContextOptions<T>) });

            if (ctor == null)
            {
                throw new ArgumentException($"Given type ({dbContextType.Name}) dose not have a public ctor that takes a DbContextOptions");
            }

            var dbContextOptions = GetDbContextOptions(config);
            var dbContext = ctor.Invoke(new object[] { dbContextOptions });

            return (T)dbContext;
        }

        public DbContextOptions<T> GetDbContextOptions(DbConfig config)
        {
            switch (config.DbEngine)
            {
                case SupportedDbEngines.Sqlite:
                    var sqLiteConfig = config as SqliteConfig;
                    if (_cloudStorage.GetObject(sqLiteConfig.DbFileName, out var dbBackup))
                    {
                        _localStorage.SaveObject(sqLiteConfig.DbFileName, dbBackup, true);
                    }

                    return GetSqliteDbOption(Path.Combine(_localStorage.Root, sqLiteConfig.DbFileName));

                case SupportedDbEngines.InMemory:
                    return GetInMemoryDbOptions(config.DbName);

                case SupportedDbEngines.Postgre:
                    var postgreSqlConfig = config as PostgreSQLConfig;
                    return GetPostgreSqlDbOptions(postgreSqlConfig);

                default: throw new ArgumentException($"Database engine {config.DbEngine} is not yet supportred");
            }
        }

        private DbContextOptions<T> GetSqliteDbOption(string file)
        {
            return new DbContextOptionsBuilder<T>()
                .UseSqlite(@$"Data Source={file}")
                .Options;
        }

        private DbContextOptions<T> GetPostgreSqlDbOptions(PostgreSQLConfig config)
        {
            return new DbContextOptionsBuilder<T>()
                .UseNpgsql($"Host={config.Host};Database={config.DbName};Username={config.Username};Password={config.Password}")
                .Options;
        }

        private DbContextOptions<T> GetSqlServerDbOptions(PostgreSQLConfig config)
        {
            return new DbContextOptionsBuilder<T>()
                .UseSqlServer($"Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;")
                .Options;
        }

        private DbContextOptions<T> GetInMemoryDbOptions(string dbKey)
        {
            return new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(dbKey)
                .Options;
        }
    }
}
