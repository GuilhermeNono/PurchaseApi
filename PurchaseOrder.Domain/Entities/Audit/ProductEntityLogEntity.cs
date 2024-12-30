using PurchaseOrder.Domain.Database.Entities;
using PurchaseOrder.Domain.Entities.Abstractions.Interfaces;
using PurchaseOrder.Domain.Entities.Main;

namespace PurchaseOrder.Domain.Entities.Audit;

public class ProductEntityLogEntity : ProductEntity, IEntityLog<long>
{
    public new long Id { get; init; }
    public Guid IdProduct { get; set; }

    public ProductEntityLogEntity()
    {
    }

    private ProductEntityLogEntity(ProductEntity entity)
    {
        IdProduct = entity.Id;
        Name = entity.Name;
        Price = entity.Price;
    }

    public static ProductEntityLogEntity FromEntity(ProductEntity entity) => new(entity);
}
