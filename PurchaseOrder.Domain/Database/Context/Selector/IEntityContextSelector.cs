using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Domain.Database.Entities;

namespace PurchaseOrder.Domain.Database.Context;

public interface IEntityContextSelector<TEntity, TId> where TEntity : class, IEntity<TId>, new()
{
    protected DbSet<TEntity> Model { get; set; }
}