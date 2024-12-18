using Microsoft.EntityFrameworkCore.Storage;

namespace PurchaseOrder.Domain.Database.Transactions;

public interface IDatabaseTransaction<TContextTransaction>
{
    TContextTransaction? CurrentTransaction { get; }
    Task<TContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
    Task RetryOnExceptionAsync(Func<Task> func);
}