namespace PurchaseOrder.Crosscutting.Exceptions.Errors.Messages;

public static class ExceptionMessage
{
    public static string ProductNotFound() => "Product not found";
    public static string MigrationFailed() => "Houve uma falha ao executar a migração do banco de dados.";
}
