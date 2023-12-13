using Microsoft.EntityFrameworkCore;
using SQLiteDemoTests.Models;

namespace SQLiteDemoTests
{
    public class TestContext : DbContext
    {
        public DbSet<SimpleModel> TestModels { get; set; }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new SimpleModelConfiguration().Configure(modelBuilder.Entity<SimpleModel>());
        }
    }
}
