using System.ComponentModel;

namespace PurchaseOrder.Domain.Enums;

public enum OperationAuditEnum
{
    [Description("Create")]
    C,
    [Description("Update")]
    U,
    [Description("Delete")]
    D
}