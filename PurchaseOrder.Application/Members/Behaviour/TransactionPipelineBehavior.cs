using MediatR;
using Microsoft.Extensions.Logging;
using PurchaseOrder.Domain.Database.Transactions;

namespace PurchaseOrder.Application.Members.Behaviour;

public class TransactionPipelineBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactional
{
    private readonly ILogger<TransactionPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly ITransactionService _transactionService;

    public TransactionPipelineBehavior(ILogger<TransactionPipelineBehavior<TRequest, TResponse>> logger,
        ITransactionService transactionService)
    {
        _logger = logger;
        _transactionService = transactionService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse? response = default;
        try
        {
            await _transactionService.ExecuteInTransactionContextAsync(async () => { response = await next(); },
                cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError("|> Ocorreu um problema durante o processo de transação:\n {Message} \n {StackTracer}",
                e.Message, e.StackTrace);
            throw;
        }

        return response!;
    }
}