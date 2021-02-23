using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiDb.ImageModels;
using MySql.Data.EntityFramework;

namespace MultiDb
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class YmPronoContext : DbContext
    {
        public DbSet<WebGallery> WebGalleries { get; set; }
        public DbSet<StoredGallery> StoredGalleries { get; set; }
        public DbSet<WebImage> WebImages { get; set; }
        public DbSet<StoredImage> StoredImages { get; set; }
<<<<<<< HEAD
        
=======

        //public DbSet<Album> Albums { get; set; }
        //public DbSet<HibridImage> HibridImages { get; set; }

>>>>>>> ba0d4ca4e3243b9116ea056b559b0b5615f5a740
        public YmPronoContext(string ctorString) : base(ctorString)
        {
            Database.SetInitializer<YmPronoContext>(new CreateDatabaseIfNotExists<YmPronoContext>());
        }

        public YmPronoContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            Database.SetInitializer<YmPronoContext>(new CreateDatabaseIfNotExists<YmPronoContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Write Fluent API configurations here
            //Configure default schema
            modelBuilder.HasDefaultSchema("viewer");

            //Map entity to table
            modelBuilder.Entity<WebGallery>().ToTable("WebGalleries");
            modelBuilder.Entity<StoredGallery>().ToTable("StoredGalleries");

            modelBuilder.Entity<WebImage>().ToTable("WebImages");
            modelBuilder.Entity<StoredImage>().ToTable("StoredImages");
        }
    }
}
