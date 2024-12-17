using System.Reflection;

namespace PurchaseOrder.Presentation;

public static class PresentationReference
{
    public static Assembly GetAssembly => typeof(PresentationReference).Assembly;
}