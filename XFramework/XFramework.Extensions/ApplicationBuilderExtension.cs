using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XFramework.Extensions.Configurations;
using XFramework.Extensions.Middlewares;

namespace XFramework.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseXFramework(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            var corsOptions = app.ApplicationServices
                .GetRequiredService<IOptions<CorsOptions>>()
                .Value;

            // 🔹 CORS
            app.UseCors(corsOptions.PolicyName);

            app.UseRouting();
            // 🔹 Swagger
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "XFramework API v1");
                });
            }

            // 🔹 Auth & Authz
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<LoggingMiddleware>();

            // 🔹 Rate Limiter
            app.UseRateLimiter();

            return app;
        }
    }
}
