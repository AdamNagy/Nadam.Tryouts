using Microsoft.EntityFrameworkCore;
using POC_DotNET_6.Models;

namespace POC_DotNET_6.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<NationalPark> NationalParks { get; set; }
        //public DbSet<Trail> Trails { get; set; }
        //public DbSet<User> Users { get; set; }
    }
}
