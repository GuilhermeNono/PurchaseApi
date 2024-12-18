namespace PurchaseOrder.Domain.Database.Transactions;

public interface ITransactionService
{
    Task ExecuteInTransactionContextAsync(Func<Task> action, CancellationToken cancellationToken);
}