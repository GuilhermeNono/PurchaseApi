using System.Text.Json.Serialization;

namespace PurchaseOrder.Domain.Exceptions.Errors.Validator;

public record ValidationError(string Description, string Code) : Error(Description, Code), IValidationError
{
    [JsonPropertyOrder(4)] public Error[]? Errors { get; set; }
}