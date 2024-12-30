using FluentValidation;
using FluentValidation.AspNetCore;
using PurchaseOrder.Api.Filters;
using PurchaseOrder.Application;
using PurchaseOrder.Crosscutting.Extensions;
using PurchaseOrder.infrastructure;
using PurchaseOrder.infrastructure.Migrations;
using PurchaseOrder.Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(opt => { opt.Filters.Add(typeof(ExceptionHandlerFilter)); })
    .AddApplicationPart(PresentationReference.GetAssembly);

#region || MediatR ||

builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssembly(ApplicationReference.GetAssembly));
builder.Services.AddBehaviours();

#endregion

#region || FluentValidation ||

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(ApplicationReference.GetAssembly, includeInternalTypes: true);

#endregion

builder.Services.AddCrosscuttingServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddRepositories();

#region || Serilog ||

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region || Migration System ||

app.RunFunctionsDbUp(builder.Configuration)
    .RunMainDbUp(builder.Configuration)
    .RunAuditDbUp(builder.Configuration);

#endregion

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseAuthorization();

app.MapControllers();

app.Run();
