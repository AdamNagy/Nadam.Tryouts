using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace SQLiteDemoTests
{
    internal class SimpleModelConfiguration : IEntityTypeConfiguration<SimpleModel>
    {
        public void Configure(EntityTypeBuilder<SimpleModel> builder)
        {
            builder.HasKey(p => p.Key);
            builder.Property(p => p.DateTimeProp).HasColumnType("date");
        }
    }
}
