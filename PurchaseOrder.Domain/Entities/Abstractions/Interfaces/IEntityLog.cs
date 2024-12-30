using PurchaseOrder.Domain.Database.Entities;

namespace PurchaseOrder.Domain.Entities.Abstractions.Interfaces;

public interface IEntityLog
{
}

public interface IEntityLog<TId> : IEntity<TId>
{
}
