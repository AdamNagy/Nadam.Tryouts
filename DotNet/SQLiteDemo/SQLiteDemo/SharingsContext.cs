using Microsoft.EntityFrameworkCore;

namespace SQLiteDemo
{
    public class SharingsContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<PeopleRelation> PeopleRelations { get; set; }

        public SharingsContext(DbContextOptions<SharingsContext> options) : base(options) { }
    }
}
