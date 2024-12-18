using MediatR;

namespace PurchaseOrder.Application.Abstractions.Query;

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse>
{
}

public interface IQueryHandler<in TRequest> : IRequestHandler<TRequest>
    where TRequest : IQuery
{
}