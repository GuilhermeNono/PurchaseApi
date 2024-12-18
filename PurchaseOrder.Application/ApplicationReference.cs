using System.Reflection;

namespace PurchaseOrder.Application;

public static class ApplicationReference
{
    public static Assembly GetAssembly => typeof(ApplicationReference).GetTypeInfo().Assembly;
}