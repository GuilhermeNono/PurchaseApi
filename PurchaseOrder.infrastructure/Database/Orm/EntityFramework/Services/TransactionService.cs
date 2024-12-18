using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PurchaseOrder.Domain.Database;
using PurchaseOrder.Domain.Database.Context;
using PurchaseOrder.Domain.Database.Transactions;

namespace PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Services;

public class TransactionService(
    IMainContext mainContext,
    IAuditContext auditContext,
    ILogger<TransactionService> logger)
    : ITransactionService
{
    private readonly Dictionary<int, IEntityTransaction?> _transactions = [];
    private readonly Stack<int> _actionHashLayers = [];
    private int? _hashCodeToFinishTransaction;

    public async Task ExecuteInTransactionContextAsync(Func<Task> action, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await BeginTransaction(cancellationToken);

        SendMessageOfTransactionStatus("|> Beginning transactions\n");

        try
        {
            int hashCode = action.GetHashCode();

            _hashCodeToFinishTransaction ??= hashCode;
            _actionHashLayers.Push(hashCode);

            await action();


            if (_actionHashLayers.Pop() == _hashCodeToFinishTransaction)
            {
                await CommitAllDbTransactions(cancellationToken);
                logger.LogInformation("|> Transaction Commited\n");
            }
        }
        catch (Exception)
        {
            SendMessageOfTransactionStatus("|> Rollback transaction executed\n", false);

            await RollbackAllDbTransactions(cancellationToken);
            throw;
        }
    }

    private void SendMessageOfTransactionStatus(string message, bool whenTransactionsNotExist = true)
    {
        if (whenTransactionsNotExist ? _transactions.IsNullOrEmpty() : !_transactions.IsNullOrEmpty())
            logger.LogInformation("{Message}", message);
    }

    private async Task BeginTransaction(CancellationToken cancellationToken)
    {
        await ComputeDbTransaction(mainContext, cancellationToken);
        await ComputeDbTransaction(auditContext, cancellationToken);
    }

    private async Task ComputeDbTransaction(IEfContext context, CancellationToken cancellationToken)
    {
        if (!HashKeyExistInTransactionList(context.GetHashCode()))
        {
            await mainContext.BeginTransactionAsync(cancellationToken);
            _transactions.Add(context.GetHashCode(), context);
        }
    }

    private async Task CommitAllDbTransactions(CancellationToken cancellationToken)
    {
        await Task.WhenAll(_transactions.Select(x => x.Value!.CommitTransactionAsync(cancellationToken))
            .ToArray());
    }

    private async Task RollbackAllDbTransactions(CancellationToken cancellationToken)
    {
        foreach (var (provider, transaction) in _transactions)
        {
            if (transaction is null) continue;

            await transaction.RollbackTransactionAsync(cancellationToken);
            _transactions.Remove(provider);
        }
    }

    private bool HashKeyExistInTransactionList(int hashKey) => _transactions.ContainsKey(hashKey);
}