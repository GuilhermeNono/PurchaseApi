using PurchaseOrder.Domain.Database.Entities;

namespace PurchaseOrder.Domain.Database.Repositories.EntityFramework;

public interface IEntityCrudRepository<TEntity, in TId> : ICrudRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>, new()
{
}