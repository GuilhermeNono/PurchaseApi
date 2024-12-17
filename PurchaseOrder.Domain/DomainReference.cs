using System.Reflection;

namespace PurchaseOrder.Api;

public static class DomainReference
{
    public static Assembly GetAssembly => typeof(DomainReference).Assembly;
}