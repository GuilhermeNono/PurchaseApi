using PurchaseOrder.Domain.Constants;

namespace PurchaseOrder.Domain.Annotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
public class ProtectedAttribute : Attribute
{
    public string Role { get; set; }

    public ProtectedAttribute(string role = UserConstant.Anonymous)
    {
        Role = role;
    }
}