using System.Data;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using XFramework.Extensions.Configurations;
using XFramework.Extensions.Configurations.ConfigurationValidations;
using XFramework.Extensions.Configurations.Configurators;
using XFramework.Extensions.Extensions;
using XFramework.Extensions.Helpers;

namespace XFramework.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddXFramework<TMain, TLog>(this IServiceCollection services, IConfiguration configuration)
            where TMain : DbContext
            where TLog : DbContext
        {

            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetSection("Jwt"))
                .ValidateOnStart();

            services.AddOptions<CorsOptions>()
                .Bind(configuration.GetSection("Cors"))
                .ValidateOnStart();

            services.AddOptions<CacheOptions>()
                .Bind(configuration.GetSection("Cache"))
                .ValidateOnStart();

            services.AddOptions<RateLimitOptions>()
                .Bind(configuration.GetSection("RateLimit"))
                .ValidateOnStart();

            services.AddOptions<EncryptionOptions>()
                .Bind(configuration.GetSection("Encryption"))
                .ValidateOnStart();

            services.AddOptions<RabbitMqOptions>().Bind(configuration.GetSection("RabbitMQ"));

            services.AddSingleton<IValidateOptions<JwtOptions>, JwtOptionsValidator>();
            services.AddSingleton<IValidateOptions<CorsOptions>, CorsOptionsValidator>();
            services.AddSingleton<IValidateOptions<CacheOptions>, CacheOptionsValidator>();
            services.AddSingleton<IValidateOptions<RateLimitOptions>, RateLimitOptionsValidator>();
            services.AddSingleton<IValidateOptions<EncryptionOptions>, EncryptionOptionsValidator>();

            services.AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtConfigurator>();

            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            services.AddAuthorization();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddValidatorsFromAssemblies(assemblies);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(assemblies);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApp API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "{token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddSingleton<ClientIpResolver>();

            var defaultConnection = configuration.GetConnectionString("DefaultConnection");
            var logConnection = configuration.GetConnectionString("LogConnection");

            if (string.IsNullOrWhiteSpace(defaultConnection))
                throw new InvalidOperationException("Missing 'DefaultConnection' in appsettings.json");
            if (string.IsNullOrWhiteSpace(logConnection))
                throw new InvalidOperationException("Missing 'LogConnection' in appsettings.json");

            services.AddDbContext<TMain>(opt => opt.UseSqlServer(defaultConnection));
            services.AddDbContext<TLog>(opt => opt.UseSqlServer(logConnection));

            var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Warning);
            services.AddSingleton(levelSwitch);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetConnectionString("LogConnection"),
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        SchemaName = "dbo",
                        AutoCreateSqlTable = true,
                        BatchPostingLimit = 50,
                        BatchPeriod = TimeSpan.FromSeconds(10)
                    },
                    columnOptions: new ColumnOptions
                    {
                        AdditionalColumns = new List<SqlColumn>
                        {
                            new("UserId", SqlDbType.NVarChar, dataLength: 100),
                            new("IPAddress", SqlDbType.NVarChar, dataLength: 50),
                            new("ActionName", SqlDbType.NVarChar, dataLength: 250),
                            new("TraceIdentifier", SqlDbType.NVarChar, dataLength: 100)
                        }
                    })
                .CreateLogger();

            services.AddCustomRateLimiter();

            return services;
        }
    }
}
