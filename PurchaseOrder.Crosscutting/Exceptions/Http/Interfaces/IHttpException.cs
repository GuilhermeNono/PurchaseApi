using System.Net;

namespace PurchaseOrder.Crosscutting.Exceptions.Http.Interfaces;

public interface IHttpException
{
    public HttpStatusCode StatusCode => default;

    public string? Code => nameof(StatusCode);
}