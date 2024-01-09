using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nadam.DataServices.Tests.EntityModels
{
    internal class FilteringModelConfiguration : IEntityTypeConfiguration<FilteringModel>
    {
        public void Configure(EntityTypeBuilder<FilteringModel> builder)
        {
            builder.HasKey(p => p.Key);
            builder.Property(p => p.DateTimeProp).HasColumnType("date");
        }
    }
}
