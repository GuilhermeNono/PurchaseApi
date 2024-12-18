using PurchaseOrder.Domain.Database.Repositories.EntityFramework;
using PurchaseOrder.Domain.Entities;

namespace PurchaseOrder.Domain.Repositories;

public interface IProductRepository : IEntityCrudRepository<ProductEntity, Guid>
{
}