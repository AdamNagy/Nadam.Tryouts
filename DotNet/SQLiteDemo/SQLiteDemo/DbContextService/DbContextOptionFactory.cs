using Microsoft.EntityFrameworkCore;
using SQLiteDemo.Config;

namespace SQLiteDemo.DbContextService
{
    public static class DbContextOptionFactory<T> where T : DbContext
    {
        public static DbContextOptions<T> GetSqliteDbOption(string file)
        {
            return new DbContextOptionsBuilder<T>()
                .UseSqlite(@$"Data Source={file}")
                .Options;
        }

        public static DbContextOptions<T> GetPostgreSqlDbOptions(string dbKey, PostgreSQLConfig config)
        {
            return new DbContextOptionsBuilder<T>()
                .UseNpgsql($"Host={config.Host};Database={config.DbName};Username={config.Username};Password={config.Password}")
                .Options;

        }

        public static DbContextOptions<T> GetInMemoryDbOptions(string dbKey)
        {
            return new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(dbKey)
                .Options;
        }
    }
}
