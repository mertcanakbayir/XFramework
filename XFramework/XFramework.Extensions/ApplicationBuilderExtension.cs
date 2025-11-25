using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using XFramework.Extensions.Middlewares;

namespace XFramework.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseXFramework(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            // 🔹 CORS
            app.UseCors("AllowAngularClient");

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
