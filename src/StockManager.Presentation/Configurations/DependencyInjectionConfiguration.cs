using StockManager.Application.Interfaces;
using StockManager.Application.Services.Balance;
using StockManager.Application.Services.Client;
using StockManager.Application.Services.Measurement;
using StockManager.Application.Services.ReceiptDocument;
using StockManager.Application.Services.ReceiptResource;
using StockManager.Application.Services.Resource;
using StockManager.Application.Services.ShipmentDocument;
using StockManager.Application.Services.ShipmentResource;
using StockManager.Infrastructure.Persistence.Repositories;
using System.ComponentModel.Design;

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
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IBalanceService, BalanceService>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IMeasurementUnitService, MeasurementUnitService>();
        builder.Services.AddScoped<IReceiptDocumentService, ReceiptDocumentService>();
        builder.Services.AddScoped<Application.Services.Resource.IResourceService, ResourceService>();
        builder.Services.AddScoped<IShipmentDocumentService, ShipmentDocumentService>();
        builder.Services.AddScoped<IReceiptResourceService, ReceiptResourceService>();
        builder.Services.AddScoped<IShipmentResourceService, ShipmentResourceService>();
    }
}
