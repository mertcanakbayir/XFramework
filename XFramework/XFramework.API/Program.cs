using System.Data;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using XFramework.API.Extensions;
using XFramework.API.Middlewares;
using XFramework.BLL.Mappings;
using XFramework.BLL.Utilities.ValidationRulers;
using XFramework.Configuration;
using XFramework.DAL;

var builder = WebApplication.CreateBuilder(args);
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:4200" };
var allowCredentials = builder.Configuration.GetValue<bool>("Cors:AllowCredentials", false);

builder.Services.AddCors(options =>
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

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtOptions = jwtSection.Get<JwtOptions>();
builder.Services.Configure<JwtOptions>(jwtSection);
builder.Services.AddAuthentication(options =>
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
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<PageAddDtoValidator>();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(cfg => { }, typeof(PageRoleProfile).Assembly);
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<XFMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddDbContext<XFrameworkLogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LogConnection"))
);

var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Warning);
builder.Services.AddSingleton(levelSwitch);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(levelSwitch)
    .Enrich.FromLogContext()
    .WriteTo.MSSqlServer(
    connectionString: builder.Configuration.GetConnectionString("LogConnection"),
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
builder.Services.AddCustomRateLimiter();
builder.Host.UseSerilog();

builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection("Cache"));

var app = builder.Build();

app.UseCors("AllowAngularClient");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.UseRoleAuthorization();

app.MapControllers();

app.Run();
