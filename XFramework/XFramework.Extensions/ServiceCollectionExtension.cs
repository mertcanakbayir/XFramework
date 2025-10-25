using System.Data;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using XFramework.BLL.Mappings;
using XFramework.BLL.Utilities.ValidationRulers;
using XFramework.Configuration;
using XFramework.DAL;
using XFramework.Extensions.Extensions;
namespace XFramework.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddXFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // 🔹 CORS
            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:4200" };
            var allowCredentials = configuration.GetValue<bool>("Cors:AllowCredentials", false);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularClient",
                    policy =>
                    {
                        policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });

            // 🔹 JWT
            var jwtSection = configuration.GetSection("Jwt");
            var jwtOptions = jwtSection.Get<JwtOptions>();
            services.Configure<JwtOptions>(jwtSection);

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

            services.AddHttpContextAccessor();
            services.AddAuthorization();

            // 🔹 Business services
            services.AddBusinessServices(configuration);

            // 🔹 Validators
            services.AddValidatorsFromAssemblyContaining<PageAddDtoValidator>();
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            // 🔹 AutoMapper
            services.AddAutoMapper(cfg => { }, typeof(PageRoleProfile).Assembly);

            // 🔹 Cache + Controllers
            services.AddMemoryCache();
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // 🔹 Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "XFramework API",
                    Version = "v1"
                });

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

            // 🔹 DbContexts
            services.AddDbContext<XFMContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<XFrameworkLogContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LogConnection")));

            // 🔹 Serilog
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
                            new SqlColumn("UserId", SqlDbType.NVarChar, dataLength: 100),
                            new SqlColumn("IPAddress", SqlDbType.NVarChar, dataLength: 50),
                            new SqlColumn("ActionName", SqlDbType.NVarChar, dataLength: 250),
                        }
                    }
                )
                .CreateLogger();

            services.AddCustomRateLimiter();
            services.Configure<CacheOptions>(configuration.GetSection("Cache"));

            return services;
        }
    }
}
