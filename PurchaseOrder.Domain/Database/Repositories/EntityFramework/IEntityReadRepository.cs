using PurchaseOrder.Domain.Database.Entities;

namespace PurchaseOrder.Domain.Database.Repositories.EntityFramework;

public interface IEntityReadRepository<TEntity, in TId> : IReadRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>, new()
{
}