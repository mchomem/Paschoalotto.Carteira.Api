using Paschoalotto.Carteira.Core.Domain.Exceptions.Base;
using DomainInvalidOperationException = Paschoalotto.Carteira.Core.Domain.Exceptions.Base.InvalidOperationException;

namespace Paschoalotto.Carteira.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu uma exceção não tratada: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Ocorreu um erro interno no servidor.";
        var errors = new List<string>();

        switch (exception)
        {
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = notFoundException.Message;
                break;

            case AlreadyExistsException alreadyExistsException:
                statusCode = HttpStatusCode.Conflict;
                message = alreadyExistsException.Message;
                break;

            case DomainInvalidOperationException invalidOperationException:
                statusCode = HttpStatusCode.BadRequest;
                message = invalidOperationException.Message;
                break;

            case DomainException domainException:
                statusCode = HttpStatusCode.BadRequest;
                message = domainException.Message;
                break;

            default:
                errors.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    errors.Add($"Inner: {exception.InnerException.Message}");
                }
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = ApiResponse<object>.Failure(message, errors);
        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        return context.Response.WriteAsync(json);
    }
}
