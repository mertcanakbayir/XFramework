# MCAkbayir.XFramework.Template

A 5-layer architecture template for rapid application development (RAD) with XFramework. Generates a ready-to-use ASP.NET Core Web API project with `dotnet new`.

## 🚀 Installation

```bash
dotnet new install MCAkbayir.XFramework.Template
```

## 📖 Usage

```bash
# Create a new project
dotnet new xframework -n MyProject

# Build the project
cd MyProject
dotnet restore
dotnet build
```

## 🏗️ Project Structure

```
MyProject/
├── 0_Common/
│   ├── MyProject.Dtos/          # DTOs organized by entity
│   └── MyProject.Helper/        # Enums, helpers, models, ViewModels
├── 1_Data/
│   └── MyProject.DAL/           # Entities, DbContext, EF configurations, seed data
├── 2_Repository/
│   └── MyProject.Repository/    # Generic repository pattern (IBaseRepository, IUnitOfWork)
├── 3_Business/
│   └── MyProject.BLL/           # Services, AutoMapper profiles, FluentValidation, extensions
├── 4_API/
│   └── MyProject.API/           # Controllers, filters, middlewares, Program.cs
└── MyProject.sln
```

## 📦 Included Packages

The template comes with **MCAkbayir.XFramework.Extensions** pre-configured in both the API and BLL layers. This gives you out-of-the-box:

- **JWT Authentication** — Bearer token setup via `appsettings.json`
- **Serilog** — Console + MSSQL logging with custom columns
- **Swagger** — JWT-enabled SwaggerUI in development
- **CORS** — Configurable CORS policies
- **Rate Limiting** — IP & user-based request throttling
- **EF Core** — Dual DbContext (Main + Log)
- **AutoMapper** — Automatic profile scanning
- **FluentValidation** — Automatic validator discovery
- **Exception & Logging Middlewares** — Global error handling and request logging

The template also includes:

| Package | Layer | Purpose |
|---------|-------|---------|
| MailKit / MimeKit | BLL | Email sending support |
| RabbitMQ.Client | BLL | Message queue integration |
| Microsoft.Extensions.Identity.Core | BLL | Identity management |
| Microsoft.EntityFrameworkCore.Design | API | EF Core migrations |

## ⚡ Quick Start

The generated `Program.cs` is minimal — XFramework handles all the boilerplate:

```csharp
builder.Services.AddXFramework<MyAppContext, MyAppLogContext>(builder.Configuration);
builder.Services.AddBusinessServices(builder.Configuration);

app.UseXFramework(builder.Environment);
app.MapControllers();
```

## 🔧 Works with XFramework Generator (Installed Separately)

The template is fully compatible with the `xgen` CLI tool for scaffolding new entities. **The Generator is not included in the template** — it must be installed separately as a global dotnet tool:

```bash
dotnet tool install --global MCAkbayir.XFramework.Generator.Tool
xgen Product
```

This generates DTOs, Mapper, Validators, Service, Controller, and DbSet — all placed in the correct template layers automatically.

## ⚠️ Important Notes

- **SQL Server is required** — The template uses `Microsoft.EntityFrameworkCore.SqlServer`. You need a running SQL Server instance.
- **`appsettings.json` must be configured before running** — Connection strings, JWT, CORS, and other settings need to be filled in. See the [Extensions README](https://www.nuget.org/packages/MCAkbayir.XFramework.Extensions) for the full configuration reference.
- **Two DbContexts are generated** — `{ProjectName}Context` for application data and `{ProjectName}LogContext` for Serilog logging, each with its own connection string.
- **Seed data is included** — The DAL layer comes with pre-defined seed data for base entities (Users, Roles, Pages, Endpoints, SystemSettings, etc.).
- **`//Generated entities` marker is in the DbContext** — This is required for the `xgen` CLI tool to know where to insert new `DbSet` entries.
- **`MCAkbayir.XFramework.Extensions` is a NuGet dependency** — It's referenced in both API and BLL layers and will be restored automatically via `dotnet restore`.

## 📄 License

MIT
