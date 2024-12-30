using PurchaseOrder.Domain.Entities;
using PurchaseOrder.Domain.Entities.Main;
using PurchaseOrder.Domain.Repositories;
using PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Context;
using PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Repositories;

namespace PurchaseOrder.infrastructure.Persistence.Repositories.Main.Product;

public class ProductRepository : EntityCrudRepository<ProductEntity, Guid, MainContext>, IProductRepository
{
    public ProductRepository(MainContext context) : base(context)
    {
    }
}