using Microsoft.EntityFrameworkCore;
using POC_DotNET_6.Models;

namespace POC_DotNET_6.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<Trail> Trails { get; set; }
        public DbSet<User> Users { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
