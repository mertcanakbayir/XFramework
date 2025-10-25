using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace XFramework.Extensions.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            var userId = context.User?.Identity?.IsAuthenticated == true ? context.User.FindFirst(ClaimTypes.NameIdentifier).Value ?? "Unknown" : "Anonymous";

            var actionName = context.GetEndpoint()?.DisplayName ?? "Unknown Action";

            LogContext.PushProperty("UserId", userId);
            LogContext.PushProperty("Action", actionName);
            LogContext.PushProperty("IPAddress", ipAddress);
            await _next(context);
        }
    }
}
