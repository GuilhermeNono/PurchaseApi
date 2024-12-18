using MediatR;

namespace PurchaseOrder.Application.Abstractions.Query;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}

public interface IQuery : IRequest
{
}