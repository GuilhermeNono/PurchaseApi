using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PurchaseOrder.Domain.Exceptions;

namespace PurchaseOrder.Api.Filters;

public class ExceptionHandlerFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var errorCatcher = context.HttpContext.RequestServices.GetService<IErrorCatcher>();
        var response = context.HttpContext.Response;

        var error = errorCatcher?.Catch(context.Exception);

        response.ContentType = "application/json";
        context.Result = new JsonResult(error) { StatusCode = error?.First().StatusCode };
    }
}