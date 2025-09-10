using Serilog;
using Serilog.Context;
using XFramework.Helper.ViewModels;

namespace XFramework.API.Middlewares
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
            var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";

            var userId = context.User?.Identity.IsAuthenticated == true ? context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : "Anonim";
            var actionName = context.GetEndpoint()?.DisplayName ?? "Bilinmeyen Action";

            LogContext.PushProperty("UserId", userId);
            LogContext.PushProperty("IPAddress", ipAddress);
            LogContext.PushProperty("Action", actionName);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Unhandled exception in {actionName}");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var result = ex switch
                {
                    KeyNotFoundException => ResultViewModel<object>.Failure("Not Found."),
                    UnauthorizedAccessException => ResultViewModel<object>.Failure("Forbidden"),
                    _ => ResultViewModel<object>.Failure("Internal Server Error")
                };

                await context.Response.WriteAsJsonAsync(result);
            }

        }
    }
}
