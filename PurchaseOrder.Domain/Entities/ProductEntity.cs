using System.ComponentModel.DataAnnotations.Schema;
using PurchaseOrder.Domain.Database.Entities;

namespace PurchaseOrder.Domain.Entities;

[Table("Product")]
public class ProductEntity : IEntity<Guid>
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}