using Microsoft.EntityFrameworkCore;
using Nadam.DataServices.Tests.EntityModels;

namespace Nadam.DataServices.Tests
{
    public class TestContext : DbContext
    {
        public DbSet<FilteringModel> TestModels { get; set; }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new FilteringModelConfiguration().Configure(modelBuilder.Entity<FilteringModel>());
        }
    }
}
