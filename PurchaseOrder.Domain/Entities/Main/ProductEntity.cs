using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Domain.Database.Entities;

namespace PurchaseOrder.Domain.Entities.Main;

[Table("Product")]
public class ProductEntity : IEntity<Guid>
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    
    [Precision(19, 4)]
    public decimal Price { get; set; }
}
