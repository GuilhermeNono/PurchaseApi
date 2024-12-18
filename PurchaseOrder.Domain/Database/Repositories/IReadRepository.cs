using PurchaseOrder.Domain.Database.Entities;

namespace PurchaseOrder.Domain.Database.Repositories;

public interface IReadRepository<TEntity, in TId> : IRepository where TEntity : class, IEntity<TId>, new()
{
    Task<TEntity?> FindById(TId id);
    Task<bool> Exists(TId id);
}