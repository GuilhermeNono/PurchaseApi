using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using PurchaseOrder.Domain;
using PurchaseOrder.Domain.Annotations;
using PurchaseOrder.Domain.Database.Context;
using PurchaseOrder.Domain.Database.Entities;
using PurchaseOrder.Domain.Enums;

namespace PurchaseOrder.infrastructure.Database.Orm.EntityFramework.Context;

public sealed class MainContext(DbContextOptions<MainContext> options, ILogger<MainContext> logger)
    : EntityBaseContext<MainContext>(options, logger), IMainContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is not "Production")
            optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        logger.LogWarning("| Initializing EF Core Main Database |");
        var entities = GetEntitiesTypes(EntityTypeEnum.Entity);
        var views = GetEntitiesTypes(EntityTypeEnum.View);

        foreach (var (entity, entityName) in entities)
            modelBuilder.Entity(entity).ToTable(entityName);
        logger.LogInformation("| EF Main Entities Loaded |");

        foreach (var (view, viewName) in views)
            modelBuilder.Entity(view)
                .HasNoKey().ToView(viewName);
        logger.LogInformation("| EF Main Views Loaded |");

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly,
            t => !t.Name.Contains("log", StringComparison.CurrentCultureIgnoreCase));
        logger.LogInformation("| EF Main Mappers Loaded |");
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

    private IEnumerable<Type> ChildrenOfBaseEntity => DomainReference
        .GetAssembly.GetTypes()
        .Where(t => t.GetInterfaces()
            .Any(it => it.IsGenericType && (it.GetGenericTypeDefinition() == typeof(IEntity<>))))
        .Where(t => t.Name.Contains("Entity"))
        .Where(t => !t.Name.Contains("Log"))
        .Where(t => !t.Name.Contains("View"))
        .Where(t => !t.IsAbstract);

    private IEnumerable<Type> ChildrenOfBaseEntityView => DomainReference
        .GetAssembly.GetTypes()
        .Where(t => t.GetInterface(nameof(IEntityView)) is not null)
        .Where(t => !t.Name.Contains("Log"));
}