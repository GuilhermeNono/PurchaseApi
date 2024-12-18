using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PurchaseOrder.Domain.Entities;
using PurchaseOrder.Domain.Repositories;
using PurchaseOrder.Presentation.Abstractions;
using PurchaseOrder.Presentation.Controllers.Responses;

namespace PurchaseOrder.Presentation.Controllers;

[Route("orders")]
public class OrdersController : ApiController
{
    private readonly IProductRepository _productRepository;

    public OrdersController(ISender sender,
        ILogger<OrdersController> log,
        IProductRepository productRepository) :
        base(sender, log)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<ProductResponse>> GetOrders()
    {
        var product = await _productRepository.FindById(Guid.Parse("90493B5A-73C4-44E1-9802-A86A73D38336")) ??
                      new ProductEntity();

        return Ok(product);
    }
}