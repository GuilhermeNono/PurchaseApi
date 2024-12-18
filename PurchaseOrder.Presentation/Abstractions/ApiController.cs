using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PurchaseOrder.Domain.Interfaces;
using PurchaseOrder.Domain.Objects;

namespace PurchaseOrder.Presentation.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase, IController
{
    protected readonly ISender Sender;
    protected readonly ILogger<IController> Log;

    protected ApiController(ISender sender, ILogger<IController> log)
    {
        Sender = sender;
        Log = log;
    }

    public LoggedPerson LoggedPerson => new(User);
}