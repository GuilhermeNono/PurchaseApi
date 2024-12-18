using System.ComponentModel.DataAnnotations.Schema;
using PurchaseOrder.Domain.Constants;
using PurchaseOrder.Domain.Database.Entities;
using PurchaseOrder.Domain.Entities.Abstractions.Interfaces;
using PurchaseOrder.Domain.Enums;

namespace PurchaseOrder.Domain.Entities.Abstractions;

public abstract class AuditableStatefulEntity<TId> : IEntity<TId>, IAudit, IStateable
{
    public TId? Id { get; init; }
    public string Operation { get; private set; } = OperationConstant.Default;
    
    [NotMapped]
    public OperationAuditEnum OperationEnum
    {
        get => Enum.Parse<OperationAuditEnum>(Operation, true);
        set => Operation = value.ToString();
    }
    public string AuditUser { get; set; } = UserConstant.System;
    public DateTime AuditDate { get; set; }
    public bool IsActive { get; protected set; } = true;
    public virtual void ChangeActiveStatus(bool newStatus = default) => IsActive = newStatus;
}