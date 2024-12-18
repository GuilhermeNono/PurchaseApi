namespace PurchaseOrder.Domain.Entities.Abstractions.Interfaces;

public interface IStateable
{
    public bool IsActive { get; }

    /// <summary>
    /// Atualiza o status da Entidade
    /// </summary>
    /// <param name="newStatus">Novo status que será atribuida à entidade.</param>
    public void ChangeActiveStatus(bool newStatus = false);
}