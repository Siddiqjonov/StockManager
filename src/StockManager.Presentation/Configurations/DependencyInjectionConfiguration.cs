using StockManager.Application.Interfaces;
using StockManager.Infrastructure.Persistence.Repositories;

namespace StockManager.Presentation.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IMeasurementUnitRepository, MeasurementUnitRepository>();
        builder.Services.AddScoped<IReceiptDocumentRepository, ReceiptDocumentRepository>();
        builder.Services.AddScoped<IReceiptResourceRepository, ReceiptResourceRepository>();
        builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
        builder.Services.AddScoped<IShipmentDocumentRepository, ShipmentDocumentRepository>();
        builder.Services.AddScoped<IShipmentResourceRepository, ShipmentResourceRepository>();
    }
}
