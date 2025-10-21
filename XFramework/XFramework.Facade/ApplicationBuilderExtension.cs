using Microsoft.AspNetCore.Builder;
using XFramework.API.Extensions;
using XFramework.API.Middlewares;

namespace XFramework.Facade
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseXFramework(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRateLimiter();
            app.UseRoleAuthorization();

            return app;
        }
    }
}
