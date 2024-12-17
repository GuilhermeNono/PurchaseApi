using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Security;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PurchaseOrder.Crosscutting.Exceptions.Http;
using PurchaseOrder.Domain.Exceptions;
using PurchaseOrder.Domain.Exceptions.Errors;
using PurchaseOrder.Domain.Exceptions.Errors.Validator;

namespace PurchaseOrder.Crosscutting.Exceptions.Catcher;

[ExcludeFromCodeCoverage]
public class ErrorCatcher(ILogger<ErrorCatcher> logger) : IErrorCatcher
{
    public IEnumerable<Error> Catch(Exception exception)
    {
        switch (exception)
        {
            case BadRequestException e:
            {
                logger.LogError("Code: exception.{Code} -> {Message} \n {StackTracer}", e.Code, e.Message,
                    e.StackTrace);
                return new List<Error>
                {
                    new(e.Message, $"exception.{e.Code}")
                    {
                        StatusCode = (int)e.StatusCode,
                        Description = e.Message,
                        Date = DateTime.Now.ToString(CultureInfo.CurrentCulture)
                    }
                };
            }
            case NotFoundException e:
            {
                logger.LogError("Code: exception.{Code} -> {Message} \n {StackTracer}", e.Code, e.Message,
                    e.StackTrace);
                return new List<Error>
                {
                    new(e.Message, $"exception.{e.Code}")
                    {
                        StatusCode = (int)e.StatusCode,
                        Description = e.Message,
                        Date = DateTime.Now.ToString(CultureInfo.CurrentCulture)
                    }
                };
            }
            case SecurityException e:
            {
                logger.LogError("Code: exception.securityException -> {Message} \n {StackTracer}", e.Message,
                    e.StackTrace);
                return new List<Error>
                {
                    new(e.Message, "exception.securityException")
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Description = e.Message,
                        Date = DateTime.Now.ToString(CultureInfo.CurrentCulture)
                    }
                };
            }

            case ValidationException e:
            {
                logger.LogError("Code: validator.errorValidation -> {Message} \n {StackTracer}", e.Message,
                    e.StackTrace);
                return new List<Error>
                {
                    new ValidationError(e.Message, "validator.errorValidation")
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Date = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                        Errors = e.Errors.Select(erro => new Error(erro.ErrorMessage, "validator.error"))
                            .ToArray()
                    }
                };
            }
            default:
            {
                logger.LogError("Code: exception.internalServer -> {Message} \n {StackTracer}", exception.Message,
                    exception.StackTrace);
                return new List<Error>
                {
                    new("Houve um erro durante a execução.", "exception.internalServer")
                    {
                        StatusCode = 500,
                        Date = DateTime.Now.ToString(CultureInfo.CurrentCulture)
                    }
                };
            }
        }
    }
}