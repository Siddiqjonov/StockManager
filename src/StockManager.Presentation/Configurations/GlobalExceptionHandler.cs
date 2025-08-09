using Microsoft.AspNetCore.Diagnostics;
using StockManager.Application.Errors;
using System.Net;
using System.Text.Json;

namespace StockManager.Presentation.Configurations;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception");

        var statusCode = exception switch
        {
            ValidationFailedException => HttpStatusCode.BadRequest,
            InvalidArgumentException => HttpStatusCode.BadRequest,
            InvalidImageException => HttpStatusCode.BadRequest,
            InvalidStateException => HttpStatusCode.Conflict,
            CannotUseSamePasswordException => HttpStatusCode.BadRequest,
            ParameterInvalidException => HttpStatusCode.BadRequest,
            DuplicateEntryException => HttpStatusCode.Conflict,
            ForbiddenException => HttpStatusCode.Forbidden,
            UnauthorizedException => HttpStatusCode.Unauthorized,
            AuthException => HttpStatusCode.Unauthorized,
            NotAllowedException => HttpStatusCode.MethodNotAllowed,
            NotFoundException => HttpStatusCode.NotFound,
            EntityNotFoundException => HttpStatusCode.NotFound,
            EmailNotValidatedException => HttpStatusCode.Forbidden,
            MailSendingException => HttpStatusCode.InternalServerError,
            NoConnectionException => HttpStatusCode.ServiceUnavailable,
            PersistenceException => HttpStatusCode.InternalServerError,
            DatabaseException => HttpStatusCode.InternalServerError,
            RemoteServerProcessingException => HttpStatusCode.BadGateway,
            ResponseStreamNullException => HttpStatusCode.BadGateway,
            ImageFileUnexpectedException => HttpStatusCode.UnsupportedMediaType,
            BaseException => HttpStatusCode.InternalServerError,
            _ => HttpStatusCode.InternalServerError
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        var response = new
        {
            StatusCode = httpContext.Response.StatusCode,
            Message = exception.Message
        };

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response), cancellationToken);
        return true;
    }
}
