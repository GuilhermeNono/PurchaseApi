using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PurchaseOrder.Domain.Database.Context;
using PurchaseOrder.Domain.Database.Transactions;
using PurchaseOrder.Domain.Repositories;
using PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Context;
using PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Services;
using PurchaseOrder.infrastructure.Persistence.Repositories.Main.Product;

namespace PurchaseOrder.infrastructure;

public static class ServiceExtension
{
    private const string ServerConnectionName = "ServerConnection";
    private const string ServerAuditConnectionName = "ServerAuditConnection";

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IMainContext, MainContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString(ServerConnectionName)));

        services.AddDbContext<IAuditContext, AuditContext>(opt =>
            {
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                opt.UseSqlServer(configuration.GetConnectionString(ServerAuditConnectionName));
            }
        );

        services.AddScoped<ITransactionService, TransactionService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }

}