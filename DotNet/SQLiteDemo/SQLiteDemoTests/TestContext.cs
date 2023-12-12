using Microsoft.EntityFrameworkCore;

namespace SQLiteDemoTests
{
    public class TestContext : DbContext
    {
        public DbSet<TestModel> TestModels { get; set; }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
