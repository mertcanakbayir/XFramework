using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using XFM.BLL.Mappings;
using XFM.BLL.Services.AuthService;
using XFM.BLL.Services.UserService;
using XFM.BLL.Utilities.Hashing;
using XFM.BLL.Utilities.JWT;
using XFM.BLL.Utilities.ValidationRulers;
using XFM.DAL;
using XFM.DAL.Abstract;
using XFM.DAL.Concrete;
using XFramework.API.Middlewares;
using XFramework.API.Services;
using XFramework.BLL.Services.MailService;
using XFramework.BLL.Services.RabbitMQService;
using XFramework.BLL.Services.RoleAuthorizationService;
using XFramework.BLL.Utilities;
using XFramework.DAL.Entities;
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

builder.Services.AddRateLimiter(options =>
{
    var ipLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        var ipResolver = httpContext.RequestServices.GetRequiredService<ClientIpResolver>();
        var ipAddress = ipResolver.GetClientIp(httpContext);

        return RateLimitPartition.GetFixedWindowLimiter(
            ipAddress,
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 20,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            });
    });

    var userLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(userId))
            return RateLimitPartition.GetNoLimiter<string>("anonymous");

        return RateLimitPartition.GetFixedWindowLimiter(
            userId,
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 50,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            });
    });

    options.GlobalLimiter = PartitionedRateLimiter.CreateChained<HttpContext>(
        new[] { ipLimiter, userLimiter }
    );

    options.OnRejected = async (context, token) =>
    {
        var ipResolver = context.HttpContext.RequestServices.GetRequiredService<ClientIpResolver>();
        var ipAddress = ipResolver.GetClientIp(context.HttpContext);
        var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";

        string message = userId == "anonymous"
            ? $"Ayný IP ({ipAddress}) üzerinden çok fazla istek yapýldý. Lütfen daha sonra deneyin."
            : $"Kullanýcý ({userId}) üzerinden çok fazla istek yapýldý. Lütfen daha sonra deneyin.";

        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsync(message, token);
    };
});

var app = builder.Build();

app.UseCors("AllowAngularClient");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRateLimiter();
app.UseAuthorization();

app.UseRoleAuthorization();

app.MapControllers();

app.Run();
