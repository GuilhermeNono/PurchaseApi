using PurchaseOrder.Domain.Exceptions.Errors;

namespace PurchaseOrder.Domain.Exceptions;

public interface IErrorCatcher
{
    public IEnumerable<Error> Catch(Exception exception);
}