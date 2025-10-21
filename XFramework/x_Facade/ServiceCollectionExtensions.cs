using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XFramework.BLL.Services.Concretes;
using XFramework.Configuration;
using XFramework.Repository.Repositories.Abstract;
using XFramework.Repository.Repositories.Concrete;

namespace x_Facade
{
    public class ServiceCollectionExtensions
    {
        public static IServiceCollection AddXFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheOptions>(configuration.GetSection("Cache"));
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            services.AddScoped<MailService>();
            services.AddScoped<SystemSettingDetailService>();
            services.AddScoped<RoleAuthorizationService>();
            services.AddScoped<MailQueueService>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddMemoryCache();

            return services;
        }
    }
}
