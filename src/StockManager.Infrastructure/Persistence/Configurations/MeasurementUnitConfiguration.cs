using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Infrastructure.Persistence.Configurations;

public class MeasurementUnitConfiguration : IEntityTypeConfiguration<MeasurementUnit>
{
    public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name)
               .IsRequired()
               .HasMaxLength(100);
        builder.HasIndex(e => e.Name)
               .IsUnique();
        builder.Property(e => e.Status)
               .HasConversion<string>()
               .IsRequired();
    }
}
