using System.Reflection;

namespace PurchaseOrder.Domain;

public static class DomainReference
{
    public static Assembly GetAssembly => typeof(DomainReference).Assembly;
}