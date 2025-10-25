using XFramework.Extensions;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddXFramework(builder.Configuration);
var app = builder.Build();
app.UseXFramework(builder.Environment);
app.MapControllers();

app.Run();
