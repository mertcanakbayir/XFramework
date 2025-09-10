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
using XFM.BLL.Mappings;
using XFM.BLL.Utilities.JWT;
using XFramework.API.Extensions;
using XFramework.API.Middlewares;
using XFramework.BLL.Services.Abstracts;
using XFramework.BLL.Services.Concretes;
using XFramework.BLL.Utilities.Hashing;
using XFramework.BLL.Utilities.ValidationRulers;
using XFramework.DAL;
using XFramework.Helper.Helpers;
using XFramework.Repository.Repositories.Abstract;
using XFramework.Repository.Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var jwtSettings = new JwtSettings();

builder.Services.Configure<JwtSettings>(options =>
{
    options.Key = jwtSettings.Key;
    options.Issuer = jwtSettings.Issuer;
    options.Audience = jwtSettings.Audience;
    options.ExpiresInMinutes = jwtSettings.ExpiresInMinutes;
});

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
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddBusinessServices(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
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
