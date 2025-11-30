using MyApp.BLL.Extensions;
using MyApp.API.Middlewares;
using MyApp.DAL;
using XFramework.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddXFramework<MyAppContext, MyAppLogContext>(builder.Configuration);
builder.Services.AddBusinessServices(builder.Configuration);
var app = builder.Build();
app.UseXFramework(builder.Environment);
app.UseMiddleware<RoleAuthorizationMiddleware>();
app.MapControllers();
app.Run();