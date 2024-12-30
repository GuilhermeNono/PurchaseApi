using PurchaseOrder.Domain.Database.Repositories.EntityFramework;
using PurchaseOrder.Domain.Entities;
using PurchaseOrder.Domain.Entities.Main;

namespace PurchaseOrder.Domain.Repositories;

public interface IProductRepository : IEntityCrudRepository<ProductEntity, Guid>
{
}