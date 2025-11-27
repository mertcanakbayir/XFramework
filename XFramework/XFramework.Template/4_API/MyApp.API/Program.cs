using MyApp.BLL.Extensions;
using MyApp.API.Middlewares;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddBusinessServices(builder.Configuration);
var app = builder.Build();

app.UseMiddleware<RoleAuthorizationMiddleware>();
app.MapControllers();
app.Run();