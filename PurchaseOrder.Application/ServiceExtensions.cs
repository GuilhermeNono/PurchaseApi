using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PurchaseOrder.Application.Members.Behaviour;

namespace PurchaseOrder.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddBehaviours(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));
        return services;
    }
}