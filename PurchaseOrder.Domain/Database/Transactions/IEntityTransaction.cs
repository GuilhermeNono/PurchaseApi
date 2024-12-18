using Microsoft.EntityFrameworkCore.Storage;

namespace PurchaseOrder.Domain.Database.Transactions;

public interface IEntityTransaction : IDatabaseTransaction<IDbContextTransaction>
{

}