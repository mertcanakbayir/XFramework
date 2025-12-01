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
            var traceId = context.TraceIdentifier;
            using (LogContext.PushProperty("UserId", userId))
            using (LogContext.PushProperty("IPAddress", ipAddress))
            using (LogContext.PushProperty("ActionName", actionName))
            using (LogContext.PushProperty("TraceIdentifier", traceId))
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Unhandled exception in {actionName}");

                    int statusCode;
                    string message;

                    if (ex is InvalidOperationException && ex.Message.Contains("[CONFLICT]"))
                    {
                        statusCode = (int)HttpStatusCode.Conflict;
                        message = ex.Message.Replace("[CONFLICT]", "").Trim();

                    }
                    else
                    {
                        statusCode = ex switch
                        {
                            KeyNotFoundException => (int)HttpStatusCode.NotFound,
                            UnauthorizedAccessException => (int)HttpStatusCode.Forbidden,
                            _ => (int)HttpStatusCode.InternalServerError
                        };

                        message = statusCode switch
                        {
                            404 => "Not Found",
                            403 => "Forbidden",
                            _ => "Internal Server Error"
                        };
                    }


                    var errorResponse = new
                    {
                        success = false,
                        message,
                        traceId,
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
