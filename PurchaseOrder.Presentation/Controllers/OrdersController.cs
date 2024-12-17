using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PurchaseOrder.Presentation.Abstractions;

namespace PurchaseOrder.Presentation.Controllers;

[Route("orders")]
public class OrdersController(ISender sender, ILogger<OrdersController> log)
    : ApiController(sender, log)
{
    [HttpGet]
    public Task<IActionResult> GetOrders()
    {
        return Task.FromResult<IActionResult>(NoContent());
    }

}