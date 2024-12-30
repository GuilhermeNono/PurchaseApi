using PurchaseOrder.Crosscutting.Exceptions.Errors.Messages;

namespace PurchaseOrder.Crosscutting.Exceptions.Internals;

public class DatabaseMigrationFailedException : Exception
{
    public DatabaseMigrationFailedException() : base(ExceptionMessage.MigrationFailed())
    {
    }
}
