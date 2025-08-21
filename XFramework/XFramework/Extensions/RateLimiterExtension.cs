using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.RateLimiting;
using XFramework.API.Services;

namespace XFramework.API.Extensions
{
    public static class RateLimiterExtension
    {
        public static IServiceCollection AddCustomRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                var ipLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                {
                    var ipResolver = httpContext.RequestServices.GetRequiredService<ClientIpResolver>();
                    var ipAddress = ipResolver.GetClientIp(httpContext);

                    return RateLimitPartition.GetFixedWindowLimiter(
                        ipAddress,
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 20,
                            Window = TimeSpan.FromMinutes(1),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 0
                        });
                });

                var userLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                {
                    var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
                    if (string.IsNullOrEmpty(userId))
                        return RateLimitPartition.GetNoLimiter<string>("anonymous");

                    return RateLimitPartition.GetFixedWindowLimiter(
                        userId,
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 50,
                            Window = TimeSpan.FromMinutes(1),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 0
                        });
                });
                options.GlobalLimiter = PartitionedRateLimiter.CreateChained<HttpContext>(
                    new[] { ipLimiter, userLimiter }
                );
                options.OnRejected = async (context, token) =>
                {
                    var ipResolver = context.HttpContext.RequestServices.GetRequiredService<ClientIpResolver>();
                    var ipAddress = ipResolver.GetClientIp(context.HttpContext);
                    var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";

                    string message = userId == "anonymous"
                        ? $"Aynı IP ({ipAddress}) üzerinden çok fazla istek yapıldı. Lütfen daha sonra deneyin."
                        : $"Kullanıcı ({userId}) üzerinden çok fazla istek yapıldı. Lütfen daha sonra deneyin.";

                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.HttpContext.Response.WriteAsync(message, token);
                };
            });

            return services;
        }
    }
}
