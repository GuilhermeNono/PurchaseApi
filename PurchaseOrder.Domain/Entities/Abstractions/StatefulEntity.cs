using PurchaseOrder.Domain.Database.Entities;
using PurchaseOrder.Domain.Entities.Abstractions.Interfaces;

namespace PurchaseOrder.Domain.Entities.Abstractions;

public abstract class StatefulEntity<TId> : IEntity<TId>, IStateable 
{
    public TId? Id { get; init; }

    public bool IsActive { get; protected set; } = true;
    public virtual void ChangeActiveStatus(bool newStatus = default) => IsActive = newStatus;
}