using System.Net;
using PurchaseOrder.Crosscutting.Exceptions.Http.Interfaces;

namespace PurchaseOrder.Crosscutting.Exceptions.Http;

public class NotFoundException : Exception, IHttpException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string? Code => nameof(StatusCode);

    public NotFoundException()
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }
}