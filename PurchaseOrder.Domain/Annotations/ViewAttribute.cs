namespace PurchaseOrder.Domain.Annotations;

public class ViewAttribute : Attribute
{
    public string Name { get; set; }

    public ViewAttribute(string name)
    {
        Name = name;
    }
}