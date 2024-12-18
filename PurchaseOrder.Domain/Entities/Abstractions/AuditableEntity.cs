using System.ComponentModel.DataAnnotations.Schema;
using PurchaseOrder.Domain.Constants;
using PurchaseOrder.Domain.Database.Entities;
using PurchaseOrder.Domain.Entities.Abstractions.Interfaces;
using PurchaseOrder.Domain.Enums;

namespace PurchaseOrder.Domain.Entities.Abstractions;

public abstract class AuditableEntity<TId> : IEntity<TId>, IAudit
{
    public TId? Id { get; init; }

    public string Operation { get; protected set; } = OperationConstant.Default;
    
    [NotMapped]
    public OperationAuditEnum OperationEnum
    {
        get => Enum.Parse<OperationAuditEnum>(Operation, true);
        set => Operation = value.ToString();
    }
    public string AuditUser { get; set; } = UserConstant.System;
    public DateTime AuditDate { get; set; }
}