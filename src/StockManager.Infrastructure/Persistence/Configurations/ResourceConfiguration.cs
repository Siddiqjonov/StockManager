using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Infrastructure.Persistence.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name)
               .IsRequired()
               .HasMaxLength(200);
        builder.HasIndex(e => e.Name)
               .IsUnique();
        builder.Property(e => e.Status)
               .HasConversion<string>()
               .IsRequired();
    }
}
