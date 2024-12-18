using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using PurchaseOrder.Domain.Constants;
using PurchaseOrder.Domain.Database;

namespace PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Context;

public abstract class EntityBaseContext<TContext> : DbContext, IDbContext where TContext : DbContext
{
    protected EntityBaseContext(DbContextOptions<TContext> options, ILogger<EntityBaseContext<TContext>> logger) : base(options)
    {
        Logger = logger;
    }

    protected ILogger<EntityBaseContext<TContext>> Logger { get; set; }
    public IDbContextTransaction? CurrentTransaction { get; private set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!EnvironmentConstant.IsDevelopmentEnvironment)
            optionsBuilder.EnableDetailedErrors(false);
        else
            optionsBuilder.EnableSensitiveDataLogging();

        optionsBuilder.ConfigureWarnings(w => w.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return CurrentTransaction ??= await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            CurrentTransaction?.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransaction();
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (CurrentTransaction != null)
                await CurrentTransaction.RollbackAsync(cancellationToken)!;
        }
        finally
        {
            await DisposeTransaction();
        }
    }

    private async Task DisposeTransaction()
    {
        if (CurrentTransaction != null)
        {
            await CurrentTransaction.DisposeAsync();
            CurrentTransaction = null;
        }
    }

    public async Task RetryOnExceptionAsync(Func<Task> func)
    {
        await Database.CreateExecutionStrategy().ExecuteAsync(func);
    }
}