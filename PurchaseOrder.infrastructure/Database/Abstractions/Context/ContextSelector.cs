using PurchaseOrder.Domain.Database.Context;

namespace PurchaseOrder.infrastructure.Database.Abstractions.Context;

public abstract class ContextSelector<TContext> : IContextSelector<TContext>
    where TContext : class, IDisposable, IAsyncDisposable
{
    public TContext Context { get; set; }

    protected ContextSelector(TContext context)
    {
        Context = context;
    }

    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}