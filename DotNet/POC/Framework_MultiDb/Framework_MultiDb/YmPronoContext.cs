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

        public DbSet<Album> Albums { get; set; }
        public DbSet<HibridImage> HibridImages { get; set; }

        public YmPronoContext(string ctorString) : base(ctorString)
        {
            
        }

        public YmPronoContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

        }
    }
}
