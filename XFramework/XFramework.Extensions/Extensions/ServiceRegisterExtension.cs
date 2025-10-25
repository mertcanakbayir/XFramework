using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XFM.BLL.Utilities.JWT;
using XFramework.BLL.Services.Abstracts;
using XFramework.BLL.Services.Concretes;
using XFramework.BLL.Utilities.Hashing;
using XFramework.Helper.Helpers;
using XFramework.Repository.Repositories.Abstract;
using XFramework.Repository.Repositories.Concrete;

namespace XFramework.Extensions.Extensions
{
    public static class ServiceRegisterExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            var bllAssembly = typeof(UserService).Assembly;

            // Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Business Services
            RegisterServices(services, bllAssembly);

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

        private static void RegisterServices(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(IRegister).IsAssignableFrom(t));
            foreach (var type in types)
            {
                services.AddScoped(type);
            }

        }
    }
}
