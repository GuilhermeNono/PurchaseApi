using System.Text.Json;
using System.Text.Json.Serialization;

namespace PurchaseOrder.Domain.Exceptions.Errors;

public record Error(
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Description,
    [property: JsonIgnore] string? Code): IError
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Date { get; set; }
    [JsonIgnore] public int? StatusCode { get; set; }
    public override string ToString() => JsonSerializer.Serialize(this);
}