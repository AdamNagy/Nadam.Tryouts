using Microsoft.EntityFrameworkCore;
using SQLiteDemo.Config;
using System.Reflection;

namespace SQLiteDemo.DbContextService
{
    public class DataContext<T> where T : DbContext
    {
        public DbConfig DbConfig { get; set; }
        public string Name { get => DbConfig.DbName; }
        public IStorageProvider BackupProvider { get; set; }

        // for SQLite first need to get the db file from storage (azure, aws, gcloud, etc) and copy it locally to a folder
        // then the location of the db file has to be seeded to SQLite Db Context Config (or copy the file where the config says)
        public LocalDriveStorageProvider LocalDriveStorage { get; set; }

        public DataContext(
            DbConfig dbConfig,
            IStorageProvider backupProvider,
            LocalDriveStorageProvider localDriveStorage)
        {
            DbConfig = dbConfig;
            LocalDriveStorage = localDriveStorage;
            BackupProvider = backupProvider;
        }

        public DbContextOptions<T> GetDbContextOptions()
        {
            switch (DbConfig.DbEngine)
            {
                case SupportedDbEngines.Sqlite:
                    var sqLiteConfig = DbConfig as SqliteConfig;
                    if (BackupProvider.GetObject(sqLiteConfig.DbFileName, out var dbBackup))
                    {
                        LocalDriveStorage.SaveObject(sqLiteConfig.DbFileName, dbBackup, true);
                    }

                    return DbContextOptionFactory<T>.GetSqliteDbOption(Path.Combine(LocalDriveStorage.Root, sqLiteConfig.DbFileName));

                case SupportedDbEngines.Postgre:
                    var postgreSqlConfig = DbConfig as PostgreSQLConfig;
                    return DbContextOptionFactory<T>.GetPostgreSqlDbOptions(postgreSqlConfig.DbName, postgreSqlConfig);

                default: throw new ArgumentException($"Database engine {DbConfig.DbEngine} is not yet supportred");
            }
        }

        public T CreateContext()
        {
            var dbContextType = typeof(T);
            var ctor = dbContextType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, new[] { typeof(DbContextOptions<T>) });

            if (ctor == null)
            {
                throw new ArgumentException($"Given type ({dbContextType.Name}) dose not have a public ctor that takes a DbContextOptions");
            }

            var dbContextOptions = GetDbContextOptions();
            var dbContext = ctor.Invoke(new object[] { dbContextOptions });

            return (T)dbContext;
        }
    }
}
