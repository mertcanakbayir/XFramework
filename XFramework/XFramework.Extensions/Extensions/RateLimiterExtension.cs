using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using XFramework.Extensions.Configurations;
using XFramework.Extensions.Helpers;


namespace XFramework.Extensions.Extensions
{
    public static class RateLimiterExtension
    {
        public static IServiceCollection AddCustomRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                var ipLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                {
                    var rateLimitOptions = httpContext.RequestServices
                      .GetRequiredService<IOptions<RateLimitOptions>>()
                      .Value;
                    var ipResolver = httpContext.RequestServices.GetRequiredService<ClientIpResolver>();
                    var ipAddress = ipResolver.GetClientIp(httpContext);

                    return RateLimitPartition.GetFixedWindowLimiter(
                        ipAddress,
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = rateLimitOptions.IpPermitLimit,
                            Window = TimeSpan.FromMinutes(rateLimitOptions.IpWindowMinutes),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 0
                        });
                });

                var userLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                {
                    var rateLimitOptions = httpContext.RequestServices
                      .GetRequiredService<IOptions<RateLimitOptions>>()
                      .Value;
                    var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
                    if (string.IsNullOrEmpty(userId))
                        return RateLimitPartition.GetNoLimiter<string>("anonymous");

                    return RateLimitPartition.GetFixedWindowLimiter(
                        userId,
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = rateLimitOptions.UserPermitLimit,
                            Window = TimeSpan.FromMinutes(rateLimitOptions.UserWindowMinutes),
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
                    var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";
                    var actionName = context.HttpContext.GetEndpoint()?.DisplayName ?? "Unknown Action";

                    Serilog.Context.LogContext.PushProperty("UserId", userId);
                    Serilog.Context.LogContext.PushProperty("Action", actionName);
                    Serilog.Context.LogContext.PushProperty("IPAddress", ipAddress);
                    {
                        Serilog.Log.Warning("Rate limit abuse: {UserId} / {IPAddress} / Action: {Action}", userId, ipAddress, actionName);
                    }

                    string message = userId == "anonymous"
                        ? $"Too many requests from IP:({ipAddress}). Please try again later."
                        : $"Too many requests from ({userId}). Please try again later.";

                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.HttpContext.Response.WriteAsync(message, token);
                };
            });

            return services;
        }
    }
}
