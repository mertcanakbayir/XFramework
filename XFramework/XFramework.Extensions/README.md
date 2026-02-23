# MCAkbayir.XFramework.Extensions

One-line infrastructure setup for your XFramework projects. Configures everything you need to get a ASP.NET Core Web API up and running.

## ✨ Features

| Feature | Description |
|---------|-------------|
| **JWT Authentication** | Automatic JWT Bearer configuration via `appsettings.json` |
| **CORS** | Flexible CORS policy management |
| **Serilog** | Console + MSSQL logging (UserId, IPAddress, ActionName, TraceIdentifier) |
| **Rate Limiter** | Request rate limiting support |
| **Swagger** | JWT-enabled SwaggerUI (Development environment) |
| **EF Core** | Dual DbContext setup (Main + Log) |
| **AutoMapper** | Assembly-based automatic profile scanning |
| **FluentValidation** | Assembly-based automatic validator discovery |
| **Exception Middleware** | Global error handling |
| **Logging Middleware** | Request/response logging |

## 🚀 Installation

```bash
dotnet add package MCAkbayir.XFramework.Extensions
```

## 📖 Usage

In your **Program.cs**:

```csharp
// Register services
builder.Services.AddXFramework<MainDbContext, LogDbContext>(builder.Configuration);

// Configure pipeline
app.UseXFramework(app.Environment);
```

**appsettings.json** configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=MainDb;...",
    "LogConnection": "Server=...;Database=LogDb;..."
  },
  "Jwt": {
    "Issuer": "MyAPI",
    "Audience": "MyClient",
    "Key": "SuperSecretKey-AtLeast32Characters!!",
    "ExpireInMinutes": 60
  },
  "Cors": {
    "PolicyName": "AllowSpecificOrigins",
    "AllowedOrigins": ["http://localhost:3000"],
    "AllowCredentials": true
  },
  "Cache": {
    "UserPageCacheMinutes": 5,
    "UserEndpointCacheMinutes": 10,
    "UseDistributed": false
  },
  "RateLimit": {
    "IpPermitLimit": 100,
    "IpWindowMinutes": 1,
    "UserPermitLimit": 50,
    "UserWindowMinutes": 1,
    "EnableRateLimiting": true
  },
  "Encryption": {
    "Key": "32-Character-Encryption-Key-Here!",
    "IV": "16CharacterIV!!!"
  },
  "RabbitMQ": {
    "Hostname": "localhost",
    "Username": "guest",
    "Password": "guest",
    "ExchangeName": "my_exchange",
    "QueueName": "my_queue",
    "RoutingKey": "my_routing_key"
  }
}
```

## ⚠️ Important Notes

- **Both `DefaultConnection` and `LogConnection` are required** — The app throws `InvalidOperationException` at startup if either is missing.
- **Jwt, Cors, Cache, RateLimit, and Encryption configs are validated on startup** — Invalid or missing values will prevent the app from starting. RabbitMQ is the only optional section.
- **Swagger is only enabled in Development** — `UseSwagger` and `UseSwaggerUI` are skipped in production.
- **The `Logs` table is created via EF migration** — `AutoCreateSqlTable` is disabled. Run `dotnet ef migrations add` and `dotnet ef database update` on the `LogContext` to create the table.
- **SQL Server is required** — Both DbContexts use `UseSqlServer`. Other providers are not supported out of the box.
- **Exception middleware maps specific exceptions** — `KeyNotFoundException` → 404, `UnauthorizedAccessException` → 403, `InvalidOperationException` with `[CONFLICT]` prefix → 409. Everything else → 500.
- **Rate limiter skips unauthenticated users for user-based limiting** — Anonymous requests are only limited by IP, not by user policy.

## 📄 License

MIT
