using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XFramework.Configuration;
using XFramework.BLL.Services.Concretes;
using XFramework.Repository.Repositories.Concrete;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.Facade
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddXFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // Options pattern
            services.Configure<CacheOptions>(configuration.GetSection("Cache"));
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            // Core services
            services.AddScoped<MailService>();
            services.AddScoped<SystemSettingDetailService>();
            services.AddScoped<RoleAuthorizationService>();
            services.AddScoped<MailQueueService>();

            // Repository
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            // MemoryCache
            services.AddMemoryCache();

            // Burada AutoMapper, FluentValidation gibi auto-register’ları da ekleyebilirsin.
            return services;
        }
    }
}
