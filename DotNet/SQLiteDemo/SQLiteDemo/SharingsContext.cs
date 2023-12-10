using Microsoft.EntityFrameworkCore;

namespace SQLiteDemo
{
    public class SharingsContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<PeopleRelation> PeopleRelations { get; set; }

        public string DbPath { get; set; }

        public SharingsContext(string dbName)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, dbName);
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
