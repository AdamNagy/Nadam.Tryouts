using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace SQLiteDemoTests
{
    internal class TestModelConfiguration : IEntityTypeConfiguration<TestModel>
    {
        public void Configure(EntityTypeBuilder<TestModel> builder)
        {
            builder.HasKey(p => p.Key);
            builder.Property(p => p.DateTimeProp).HasColumnType("date");
        }
    }
}
