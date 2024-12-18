using Microsoft.EntityFrameworkCore.Infrastructure;
using PurchaseOrder.Domain.Database.Transactions;

namespace PurchaseOrder.Domain.Database;

public interface IEfContext : IDbContext, IEntityTransaction
{
    DatabaseFacade Database { get; }
}