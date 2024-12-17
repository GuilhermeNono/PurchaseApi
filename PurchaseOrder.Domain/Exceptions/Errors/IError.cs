namespace PurchaseOrder.Domain.Exceptions.Errors;

public interface IError
{
    public string? Code { get; init; }
    public string? Description { get; init; }
    public string? Date { get; set; }
    public int? StatusCode { get; set; }
}