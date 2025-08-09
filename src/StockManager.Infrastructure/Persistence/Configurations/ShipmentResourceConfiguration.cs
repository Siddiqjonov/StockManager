using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Infrastructure.Persistence.Configurations;

public class ShipmentResourceConfiguration : IEntityTypeConfiguration<ShipmentResource>
{
    public void Configure(EntityTypeBuilder<ShipmentResource> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.ShipmentDocument)
               .WithMany(d => d.Resources)
               .HasForeignKey(e => e.ShipmentDocumentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Resource)
               .WithMany(r => r.ShipmentResources)
               .HasForeignKey(e => e.ResourceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.MeasurementUnit)
               .WithMany(m => m.ShipmentResources)
               .HasForeignKey(e => e.MeasurementUnitId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
