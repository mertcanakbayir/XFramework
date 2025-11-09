using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;
using System.Net;
using System.Text.Json;

namespace XFramework.Extensions.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var userId = context.User?.Identity?.IsAuthenticated == true
                ? context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                : "Anonymous";
            var actionName = context.GetEndpoint()?.DisplayName ?? "Unknown Action";

            using (LogContext.PushProperty("UserId", userId))
            using (LogContext.PushProperty("IPAddress", ipAddress))
            using (LogContext.PushProperty("Action", actionName))
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Unhandled exception in {actionName}");

                    var statusCode = ex switch
                    {
                        KeyNotFoundException => (int)HttpStatusCode.NotFound,
                        UnauthorizedAccessException => (int)HttpStatusCode.Forbidden,
                        _ => (int)HttpStatusCode.InternalServerError
                    };

                    var errorResponse = new
                    {
                        success = false,
                        message = statusCode switch
                        {
                            404 => "Not Found",
                            403 => "Forbidden",
                            _ => "Internal Server Error"
                        },
                        traceId = context.TraceIdentifier,
                        action = actionName,
                        userId,
                        ipAddress
                    };

                    context.Response.StatusCode = statusCode;
                    context.Response.ContentType = "application/json";
                    var json = JsonSerializer.Serialize(errorResponse);
                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}
