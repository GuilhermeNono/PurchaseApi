using PurchaseOrder.Domain.Database.Entities;
using PurchaseOrder.Domain.Database.Repositories;

namespace PurchaseOrder.infrastructure.Database.Abstractions.Repositories;

internal abstract class CrudRepository<TEntity, TId> : ReadRepository<TEntity, TId>,
    ICrudRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>, new()
{
    public abstract Task<int> Delete(TEntity entity, CancellationToken cancellationToken);

    public abstract Task<int> Delete(TId id, CancellationToken cancellationToken);

    public abstract Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken);

    public abstract Task<TEntity> Update(TEntity entity, string userWhoUpdated, CancellationToken cancellationToken);

    public abstract Task<IEnumerable<TEntity>> Update(IList<TEntity> entities, string userWhoUpdated,
        CancellationToken cancellationToken);

    public abstract Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken);

    public abstract Task<TEntity> Add(TEntity entity, string userWhoAdded, CancellationToken cancellationToken);

    public abstract Task<IEnumerable<TEntity>> AddMany(IList<TEntity> entity, string userWhoAdded,
        CancellationToken cancellationToken);
}