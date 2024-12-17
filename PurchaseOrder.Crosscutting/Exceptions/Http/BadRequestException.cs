using System.Net;
using PurchaseOrder.Crosscutting.Exceptions.Http.Interfaces;

namespace PurchaseOrder.Crosscutting.Exceptions.Http;

public class BadRequestException : Exception, IHttpException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string? Code => nameof(StatusCode);

    public BadRequestException()
    {
    }

    public BadRequestException(string? message) : base(message)
    {
    }
}