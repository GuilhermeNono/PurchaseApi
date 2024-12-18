using Microsoft.Extensions.DependencyInjection;
using PurchaseOrder.Crosscutting.Exceptions.Catcher;
using PurchaseOrder.Domain.Exceptions;

namespace PurchaseOrder.Crosscutting.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCrosscuttingServices(this IServiceCollection services)
    {
        services.AddScoped<IErrorCatcher, ErrorCatcher>();

        return services;
    }
}