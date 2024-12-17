namespace PurchaseOrder.Domain.Exceptions.Errors.Validator;

public interface IValidationError: IError
{
    public Error[]? Errors { get; set; }
}