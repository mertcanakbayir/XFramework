using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.BLL.Services.Abstracts;
using MyApp.BLL.Services.Concretes;
using MyApp.BLL.Utilities.Hashing;
using MyApp.BLL.Utilities.JWT;
using MyApp.Helper.Helpers;
using MyApp.Repository.Repositories.Abstract;
using MyApp.Repository.Repositories.Concrete;

namespace MyApp.BLL.Extensions
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
            services.AddScoped<CurrentUserProvider>();
            services.AddSingleton<ClientIpResolver>();

            // Mail Services
            services.AddTransient<MailService>();
            services.AddSingleton<MailQueueService>();
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
