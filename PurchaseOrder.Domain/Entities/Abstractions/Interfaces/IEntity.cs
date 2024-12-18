namespace PurchaseOrder.Domain.Database.Entities;

public interface IEntity<TId>
{
    public TId? Id { get; init; }
}