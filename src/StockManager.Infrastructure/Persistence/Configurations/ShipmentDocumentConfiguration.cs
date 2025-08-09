using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Infrastructure.Persistence.Configurations;

public class ShipmentDocumentConfiguration : IEntityTypeConfiguration<ShipmentDocument>
{
    public void Configure(EntityTypeBuilder<ShipmentDocument> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Number)
               .IsRequired()
               .HasMaxLength(50);
        builder.HasIndex(e => e.Number)
               .IsUnique();
        builder.Property(e => e.Status)
               .HasConversion<string>()
               .IsRequired();

        builder.HasOne(e => e.Client)
               .WithMany(c => c.ShipmentDocuments)
               .HasForeignKey(e => e.ClientId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
