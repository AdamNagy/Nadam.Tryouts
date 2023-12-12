using Microsoft.EntityFrameworkCore;

namespace SQLiteDemo
{
    public class SharingsContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<PeopleRelation> PeopleRelations { get; set; }

        //public SharingsContext()
        //{
        //    Database.EnsureCreated();
        //}

        public SharingsContext(DbContextOptions<SharingsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlite($"Data Source={DbPath}");

        //}
    }
}
