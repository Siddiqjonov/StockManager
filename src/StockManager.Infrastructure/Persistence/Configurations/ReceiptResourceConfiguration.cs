using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Infrastructure.Persistence.Configurations;

public class ReceiptResourceConfiguration : IEntityTypeConfiguration<ReceiptResource>
{
    public void Configure(EntityTypeBuilder<ReceiptResource> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.ReceiptDocument)
               .WithMany(d => d.Resources)
               .HasForeignKey(e => e.ReceiptDocumentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Resource)
               .WithMany(r => r.ReceiptResources)
               .HasForeignKey(e => e.ResourceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.MeasurementUnit)
               .WithMany(m => m.ReceiptResources)
               .HasForeignKey(e => e.MeasurementUnitId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
