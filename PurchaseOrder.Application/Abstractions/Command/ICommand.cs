using MediatR;
using PurchaseOrder.Domain.Database.Transactions;

namespace PurchaseOrder.Application.Abstractions.Command;

public interface ICommand<out TResponse> : IRequest<TResponse>, ITransactional
{
}

public interface ICommand : IRequest, ITransactional
{
}