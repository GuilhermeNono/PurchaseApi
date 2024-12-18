using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Domain.Database.Context;
using PurchaseOrder.Domain.Database.Entities;
using PurchaseOrder.infrastructure.Database.Abstractions.Context;

namespace PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Context.Selector;

public abstract class EntityContextSelector<TEntity, TId, TContext> : ContextSelector<TContext>, IEntityContextSelector<TEntity, TId>
    where TContext : DbContext, IDisposable, IAsyncDisposable
    where TEntity : class, IEntity<TId>, new()
{

    public DbSet<TEntity> Model { get; set; }

    protected EntityContextSelector(TContext context) : base(context)
    {
        Model = context.Set<TEntity>();
        Context.ChangeTracker.AutoDetectChangesEnabled = false;
        Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

}