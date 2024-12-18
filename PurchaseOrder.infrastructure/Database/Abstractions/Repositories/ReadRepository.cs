using PurchaseOrder.Domain.Database.Entities;
using PurchaseOrder.Domain.Database.Repositories;

namespace PurchaseOrder.infrastructure.Database.Abstractions.Repositories;

internal abstract class ReadRepository<TEntity, TId> : IReadRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>, new()
{
    public abstract Task<TEntity?> FindById(TId id);

    public abstract Task<bool> Exists(TId id);
}