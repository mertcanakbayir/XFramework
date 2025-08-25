using System.Data;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using XFM.BLL.Mappings;
using XFM.BLL.Utilities.Hashing;
using XFM.BLL.Utilities.JWT;
using XFramework.API.Extensions;
using XFramework.API.Middlewares;
using XFramework.API.Services;
using XFramework.BLL.Services.Abstracts;
using XFramework.BLL.Services.Concretes;
using XFramework.BLL.Utilities;
using XFramework.BLL.Utilities.ValidationRulers;
using XFramework.DAL;
using XFramework.DAL.Abstract;
using XFramework.DAL.Concrete;

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
//??????
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanCreateOrder", policy =>
      policy.RequireClaim("Permission", "CreateOrder"));
});

// Add services to the container.
builder.Services.AddScoped<IHashingHelper, HashingHelper>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenHelper, TokenHelper>();
builder.Services.AddScoped<RoleAuthorizationService>();
builder.Services.AddSingleton<ClientIpResolver>();
builder.Services.AddTransient<MailService>();
builder.Services.AddScoped<EncryptionHelper>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<PageService>();
builder.Services.AddScoped<EndpointService>();
builder.Services.AddSingleton<MailQueueService>(sp =>
{
    var config = builder.Configuration.GetSection("RabbitMQ");

    var host = config["hostname"];
    var user = config["username"];
    var pass = config["password"];

    return new MailQueueService(host, user, pass);
});
builder.Services.AddMemoryCache();

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

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

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
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
app.UseRateLimiter();
app.UseAuthorization();
app.UseRoleAuthorization();

app.MapControllers();

app.Run();
