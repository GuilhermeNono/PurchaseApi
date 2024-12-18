using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using PurchaseOrder.Domain;
using PurchaseOrder.Domain.Annotations;
using PurchaseOrder.Domain.Database.Context;
using PurchaseOrder.Domain.Database.Entities;
using PurchaseOrder.Domain.Enums;

namespace PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Context;

public sealed class AuditContext(DbContextOptions<AuditContext> options, ILogger<AuditContext> logger)
    : EntityBaseContext<AuditContext>(options, logger), IAuditContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        logger.LogWarning("| Initializing EF Core Audit Database |");

        var entities = GetEntitiesTypes(EntityTypeEnum.Entity);
        var views = GetEntitiesTypes(EntityTypeEnum.View);

        foreach (var (entity, name) in entities)
            modelBuilder.Entity(entity).ToTable(name);
        logger.LogInformation("| EF Audit Entities Loaded |");

        foreach (var (view, name) in views)
            modelBuilder.Entity(view).HasNoKey().ToView(name);
        logger.LogInformation("| EF Audit Views Loaded |");

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly,
            t => t.Name.Contains("log", StringComparison.CurrentCultureIgnoreCase));
        logger.LogInformation("| EF Audit Mappers Loaded |");
    }

    private Dictionary<Type, string> GetEntitiesTypes(EntityTypeEnum type) => JoinAllChildren(type);

    private Dictionary<Type, string> JoinAllChildren(EntityTypeEnum type)
    {
        var children = new Dictionary<Type, string>();

        switch (type)
        {
            case EntityTypeEnum.Entity:
            {
                var entities = ChildrenOfBaseEntity.ToList();

                foreach (Type entity in entities)
                    children.Add(entity,
                        ((TableAttribute)Attribute.GetCustomAttribute(entity, typeof(TableAttribute))!).Name);

                return children;
            }
            case EntityTypeEnum.View:
            {
                var views = ChildrenOfBaseEntityView.ToList();

                foreach (Type view in views)
                    children.Add(view,
                        ((ViewAttribute)Attribute.GetCustomAttribute(view, typeof(ViewAttribute))!).Name);

                return children;
            }
            default:
                return children;
        }
    }

    private static IEnumerable<Type> ChildrenOfBaseEntity => DomainReference
        .GetAssembly.GetTypes()
        .Where(t => t.BaseType != null)
        .Where(t => t.Name.Contains("LogEntity"));

    private static IEnumerable<Type> ChildrenOfBaseEntityView => DomainReference
        .GetAssembly.GetTypes()
        .Where(t => t.GetInterface(nameof(IEntityView)) is not null)
        .Where(t => t.Name.Contains("Log"));
}