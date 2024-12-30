using System.Text;
using DbUp;
using DbUp.Engine;
using DbUp.ScriptProviders;
using Microsoft.AspNetCore.Builder;
using DbUp.Support;
using Microsoft.Extensions.Configuration;
using PurchaseOrder.Crosscutting.Exceptions.Internals;
using PurchaseOrder.Domain.Constants;

namespace PurchaseOrder.infrastructure.Migrations;

public static class MigrationConfiguration
{
    private const string ServerConnectionName = "SqlServerConnection";
    private const string AuditServerConnectionName = "SqlAuditServerConnection";
    
    private static readonly IReadOnlyList<string> ServerConnections = [ServerConnectionName, AuditServerConnectionName];

    public static IApplicationBuilder RunFunctionsDbUp(this IApplicationBuilder application,
        IConfiguration configuration)
    {
        Console.WriteLine("| Checando funções customizadas dos Bancos |");

        foreach (var connectionName in ServerConnections)
        {
            var connectionString = configuration.GetConnectionString(connectionName);

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader = ConfigureEngine(connectionString!, "Functions", "Versions");

            var result = upgrader.PerformUpgrade();
            if (!result.Successful)
                throw new DatabaseMigrationFailedException();
        }

        Console.WriteLine("| Checagem das funções finalizadas |\n");

        return application;
    }


    public static IApplicationBuilder RunMainDbUp(this IApplicationBuilder application, IConfiguration configuration)
    {
        Console.WriteLine("| Checando arquivos de migração do Banco |");
        var connectionString = configuration.GetConnectionString(ServerConnectionName);

        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader = ConfigureEngine(connectionString!, "Main", "Versions");

        var result = upgrader.PerformUpgrade();
        if (!result.Successful)
            throw new DatabaseMigrationFailedException();
        Console.WriteLine("| Checagem de migração do Banco Finalizada |\n");

        return application;
    }

    public static IApplicationBuilder RunAuditDbUp(this IApplicationBuilder application, IConfiguration configuration)
    {
        Console.WriteLine("| Checando arquivos de migração do Banco de Auditoria |");
        var connectionString = configuration.GetConnectionString(AuditServerConnectionName);

        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader = ConfigureEngine(connectionString!, "Audit", "Versions");

        var result = upgrader.PerformUpgrade();
        
        if (!result.Successful)
            throw new DatabaseMigrationFailedException();
        
        Console.WriteLine("| Checagem de migração do Banco de Auditoria Finalizada |\n");

        return application;
    }

    private static UpgradeEngine ConfigureEngine(string connectionString, params string[] folder) => DeployChanges.To
        .SqlDatabase(connectionString)
        .WithScriptsFromFileSystem(Path.Combine([AppDomain.CurrentDomain.BaseDirectory, "Migrations", "Scripts", .. folder]),
            new FileSystemScriptOptions
            {
                IncludeSubDirectories = true,
                Extensions = ["*.sql"], 
                Encoding = Encoding.UTF8
            })
        .WithFilter(new ExecutionOrderScriptFilter())
        .WithTransactionPerScript()
        .Build();
}

public class ExecutionOrderScriptFilter : IScriptFilter
{
    private const string ScriptFileName = "script";
    private const string TempScriptFileName = "Temp";

    public IEnumerable<SqlScript> Filter(
        IEnumerable<SqlScript> sorted,
        HashSet<string> executedScriptNames,
        ScriptNameComparer comparer)
    {
        return sorted
            .Where(s => s.SqlScriptOptions.ScriptType == ScriptType.RunAlways ||
                        !executedScriptNames.Contains(s.Name, comparer)).Where(x =>
                !EnvironmentConstant.IsProductionEnvironment || x.Name.Contains(ScriptFileName) ||
                (!x.Name.Contains(ScriptFileName) && !x.Name.Contains(TempScriptFileName)))
            .OrderByDescending(str => str.Name.Contains(ScriptFileName));
    }
}
