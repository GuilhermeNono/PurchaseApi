using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Domain.Database.Entities;
using PurchaseOrder.Domain.Database.Repositories;

namespace PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Repositories;

public abstract class EntityCrudRepository<TEntity, TId, TContext> : EntityReadRepository<TEntity, TId, TContext>,
    ICrudRepository<TEntity, TId>
    where TContext : DbContext, IDisposable, IAsyncDisposable
    where TEntity : class, IEntity<TId>, new()
{
    protected EntityCrudRepository(TContext context) : base(context)
    {
    }

    public Task<int> Delete(TEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<int> Delete(TId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> Update(TEntity entity, string userWhoUpdated, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> Update(IList<TEntity> entities, string userWhoUpdated, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> Add(TEntity entity, string userWhoAdded, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> AddMany(IList<TEntity> entity, string userWhoAdded, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}