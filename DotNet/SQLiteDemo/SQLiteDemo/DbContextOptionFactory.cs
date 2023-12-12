using Microsoft.EntityFrameworkCore;
using SQLiteDemo.Config;

namespace SQLiteDemo
{
    public static class DbContextOptionFactory<T> where T : DbContext
    {
        private static readonly Dictionary<string, DbContextOptions<T>> _options = new Dictionary<string, DbContextOptions<T>>();

        public static DbContextOptions<T> GetSqliteDbOption(string file)
        {
            if (_options.ContainsKey(file)) return _options[file];

            var contextOptions = new DbContextOptionsBuilder<T>()
                .UseSqlite(@$"Data Source={file}")
                .Options;

            _options.Add(file, contextOptions);
            return _options[file];
        }

        public static DbContextOptions<T> GetPostgreSqlDbOptions(string dbKey, PostgreSqlConfig config)
        {
            if (_options.ContainsKey(dbKey)) return _options[dbKey];

            var contextOptions = new DbContextOptionsBuilder<T>()
                .UseNpgsql($"Host={config.Host};Database={config.DbName};Username={config.Username};Password={config.Password}")
                .Options;

            _options.Add(dbKey, contextOptions);
            return _options[dbKey];
        }

        public static DbContextOptions<T> GetInMemoryDbOptions(string dbKey)
        {
            if (_options.ContainsKey(dbKey)) return _options[dbKey];

            var contextOptions = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(dbKey)
                .Options;

            _options.Add(dbKey, contextOptions);
            return _options[dbKey];
        }
    }
}
