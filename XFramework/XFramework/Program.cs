using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XFM.BLL.Mappings;
using XFM.BLL.Services.AuthService;
using XFM.BLL.Services.UserService;
using XFM.BLL.Utilities.Hashing;
using XFM.BLL.Utilities.JWT;
using XFM.BLL.Utilities.ValidationRulers;
using XFM.DAL;
using XFM.DAL.Abstract;
using XFM.DAL.Concrete;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IHashingHelper, HashingHelper>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenHelper, TokenHelper>();

builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<XFMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
