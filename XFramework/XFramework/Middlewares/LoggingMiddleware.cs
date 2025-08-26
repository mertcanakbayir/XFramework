using System.Security.Claims;
using Serilog.Context;

namespace XFramework.API.Middlewares
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
            var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";

            var userId = context.User?.Identity?.IsAuthenticated == true ? context.User.FindFirst(ClaimTypes.NameIdentifier).Value ?? "Bilinmiyor" : "Anonim";

            var actionName = context.GetEndpoint()?.DisplayName ?? "Bilinmeyen Action";

            LogContext.PushProperty("UserId", userId);
            LogContext.PushProperty("Action", actionName);
            LogContext.PushProperty("IPAddress", ipAddress);
            await _next(context);
        }
    }
}
