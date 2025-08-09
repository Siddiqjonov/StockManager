using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name)
               .IsRequired()
               .HasMaxLength(200);
        builder.HasIndex(e => e.Name)
               .IsUnique();
        builder.Property(e => e.Address)
               .HasMaxLength(300);
        builder.Property(e => e.Status)
               .HasConversion<string>()
               .IsRequired();
    }
}
