using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Infrastructure.Persistence.Configurations;

public class ReceiptDocumentConfiguration : IEntityTypeConfiguration<ReceiptDocument>
{
    public void Configure(EntityTypeBuilder<ReceiptDocument> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Number)
               .IsRequired()
               .HasMaxLength(50);
        builder.HasIndex(e => e.Number)
               .IsUnique();
    }
}
