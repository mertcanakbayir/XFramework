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
            // 🔹 1. Configuration Binding
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.Configure<CorsOptions>(configuration.GetSection("Cors"));
            services.Configure<CacheOptions>(configuration.GetSection("Cache"));
            services.Configure<RateLimitOptions>(configuration.GetSection("RateLimit"));

            // 🔹 2. Options as Concrete Types (for DI)
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtOptions>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<CorsOptions>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<CacheOptions>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<RateLimitOptions>>().Value);

            // 🔹 3. CORS
            var corsOptions = configuration.GetSection("Cors").Get<CorsOptions>() ?? new CorsOptions();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowClient", policy =>
                {
                    policy.WithOrigins(corsOptions.AllowedOrigins ?? new[] { "http://localhost:4200" })
                          .AllowAnyHeader()
                          .AllowAnyMethod();

                    if (corsOptions.AllowCredentials)
                        policy.AllowCredentials();
                });
            });

            // 🔹 4. JWT Authentication
            var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>() ?? new JwtOptions();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key ?? "")),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            // 🔹 5. Validators & AutoMapper
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddValidatorsFromAssemblies(assemblies);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(assemblies);
            });

            // 🔹 6. Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "XFramework API", Version = "v1" });

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

            // 🔹 7. DbContexts
            var defaultConnection = configuration.GetConnectionString("DefaultConnection");
            var logConnection = configuration.GetConnectionString("LogConnection");

            if (string.IsNullOrWhiteSpace(defaultConnection))
                throw new InvalidOperationException("Missing 'DefaultConnection' in appsettings.json");
            if (string.IsNullOrWhiteSpace(logConnection))
                throw new InvalidOperationException("Missing 'LogConnection' in appsettings.json");

            services.AddDbContext<TMain>(opt => opt.UseSqlServer(defaultConnection));
            services.AddDbContext<TLog>(opt => opt.UseSqlServer(logConnection));


            // 🔹 8. Serilog
            var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Warning);
            services.AddSingleton(levelSwitch);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .Enrich.FromLogContext()
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
                            new("ActionName", SqlDbType.NVarChar, dataLength: 250)
                        }
                    })
                .CreateLogger();

            // 🔹 9. Rate Limiter
            services.AddCustomRateLimiter(configuration);

            return services;
        }
    }
}
