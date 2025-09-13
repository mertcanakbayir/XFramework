using XFM.BLL.Utilities.JWT;
using XFramework.BLL.Services.Abstracts;
using XFramework.BLL.Services.Concretes;
using XFramework.BLL.Utilities.Hashing;
using XFramework.Dtos;
using XFramework.Helper.Helpers;
using XFramework.Repository.Repositories.Abstract;
using XFramework.Repository.Repositories.Concrete;

namespace XFramework.API.Extensions
{
    public static class ServiceRegisterExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Business Services

            services.AddScoped<UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<RoleService>();
            services.AddScoped<EndpointService>();
            services.AddScoped<SystemSettingService>();
            services.AddScoped<SystemSettingDetailService>();
            services.AddScoped<LogSettingsService>();
            services.AddScoped<RoleAuthorizationService>();
            services.AddScoped<PageService>();

            // Utilities and Helpers
            services.AddScoped<IHashingHelper, HashingHelper>();
            services.AddScoped<ITokenHelper, TokenHelper>();
            services.AddScoped<EncryptionHelper>();
            services.AddScoped<CurrentUserProvider>();
            services.AddSingleton<ClientIpResolver>();

            // Mail Services
            services.AddTransient<MailService>();
            services.AddSingleton<MailQueueService>(sp =>
            {
                var config = configuration.GetSection("RabbitMQ");
                var host = config["hostname"];
                var user = config["username"];
                var pass = config["password"];
                return new MailQueueService(host, user, pass);
            });
            return services;
        }
    }
}
