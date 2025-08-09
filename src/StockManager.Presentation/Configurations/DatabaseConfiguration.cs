using Microsoft.EntityFrameworkCore;
using StockManager.Infrastructure.Persistence.DataContext;
using System;

namespace StockManager.Presentation.Configurations;

public static class DatabaseConfiguration
{
    public static void ConfigureSqlServerDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");

        builder.Services.AddDbContext<MainContext>(options =>
            options.UseSqlServer(connectionString));
    }
}
