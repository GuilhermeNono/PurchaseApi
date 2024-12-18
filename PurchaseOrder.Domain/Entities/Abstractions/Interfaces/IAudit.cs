using PurchaseOrder.Domain.Enums;

namespace PurchaseOrder.Domain.Entities.Abstractions.Interfaces;

public interface IAudit
{
    public string Operation { get; }
    public OperationAuditEnum OperationEnum { get; set; }
    public string AuditUser { get; set; }
    public DateTime AuditDate { get; set; }
}