using System.Net;
using System.Text.Json;

namespace AiServices.Api.Middleware;

/// <summary>
/// Middleware gom lỗi thành JSON thống nhất để WPF/ERP client dễ xử lý.
/// Sau này có thể mở rộng thêm errorCode, traceId, auditId.
/// </summary>
public sealed class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionMiddleware> _logger;

    public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
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
        catch (UnauthorizedAccessException ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.Unauthorized, "UNAUTHORIZED", ex.Message);
        }
        catch (ArgumentException ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.BadRequest, "BAD_REQUEST", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled API exception.");
            await WriteErrorAsync(context, HttpStatusCode.InternalServerError, "INTERNAL_ERROR", ex.Message);
        }
    }

    private static async Task WriteErrorAsync(HttpContext context, HttpStatusCode statusCode, string errorCode, string message)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json; charset=utf-8";

        var payload = new
        {
            errorCode,
            message,
            traceId = context.TraceIdentifier
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
}
