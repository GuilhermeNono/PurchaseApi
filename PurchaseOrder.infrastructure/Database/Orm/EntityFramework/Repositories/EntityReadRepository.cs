using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Domain.Database.Entities;
using PurchaseOrder.Domain.Database.Repositories;
using PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Context.Selector;

namespace PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Repositories;

public abstract class EntityReadRepository<TEntity, TId, TContext> : EntityContextSelector<TEntity, TId, TContext>,
    IReadRepository<TEntity, TId>
    where TContext : DbContext, IDisposable, IAsyncDisposable
    where TEntity : class, IEntity<TId>, new()
{
    protected EntityReadRepository(TContext context) : base(context)
    {
    }

    public Task<TEntity?> FindById(TId id)
    {
        return Task.FromResult(Context.Find<TEntity>(id));
    }

    public Task<bool> Exists(TId id)
    {
        return Task.FromResult(Context.Find<TEntity>(id) is not null);
    }
}