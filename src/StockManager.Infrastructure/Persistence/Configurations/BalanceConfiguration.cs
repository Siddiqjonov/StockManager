using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Infrastructure.Persistence.Configurations;

public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
{
    public void Configure(EntityTypeBuilder<Balance> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Resource)
               .WithMany(r => r.Balances)
               .HasForeignKey(e => e.ResourceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.MeasurementUnit)
               .WithMany(m => m.Balances)
               .HasForeignKey(e => e.MeasurementUnitId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

