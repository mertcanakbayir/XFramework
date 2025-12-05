using XFramework.API.Middlewares;
using XFramework.BLL.Extensions;
using XFramework.DAL;
using XFramework.Extensions;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddXFramework<XFMContext, XFrameworkLogContext>(builder.Configuration);
var app = builder.Build();
app.UseXFramework(builder.Environment);
app.UseMiddleware<RoleAuthorizationMiddleware>();
app.MapControllers();

app.Run();
