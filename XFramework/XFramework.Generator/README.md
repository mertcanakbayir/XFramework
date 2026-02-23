# MCAkbayir.XFramework.Generator.Tool

CLI tool for automatic code generation in XFramework projects. Scaffolds a full CRUD stack from a single entity — DTOs, Mapper, Validators, Service, Controller, and DbSet — with one command.

## 🚀 Installation

```bash
dotnet tool install --global MCAkbayir.XFramework.Generator.Tool
```

## 📖 Usage

Run from your project directory (where the `*.sln` file is located):

```bash
# Generate all components for an entity
xgen Product

# Skip specific components
xgen Product --skip Mapper
xgen Product --skip Mapper,Validator

# Run via dotnet
dotnet xgen Order --skip Controller,DbSet
```

### Available `--skip` values

`Dto`, `Mapper`, `Validator`, `Service`, `Controller`, `DbSet`

## ⚙️ Generated Components

| Component | Output Path | What Gets Generated |
|-----------|-------------|---------------------|
| **DTO** | `Dtos/{Entity}/` | `EntityDto`, `EntityAddDto`, `EntityUpdateDto` |
| **Mapper** | `BLL/Mappings/` | AutoMapper `Profile` with reverse maps for all DTOs |
| **Validator** | `BLL/Utilities/ValidationRulers/{Entity}/` | FluentValidation `AbstractValidator` for AddDto & UpdateDto |
| **Service** | `BLL/Services/Concretes/` | `EntityService` extending `BaseService` with full CRUD |
| **Controller** | `API/Controllers/` | REST controller with `GET`, `POST`, `PUT`, `DELETE` endpoints |
| **DbSet** | `DAL/{Project}Context.cs` | Adds `DbSet<Entity>` to your DbContext |

## 🔍 How It Works

1. **Builds DAL** — Automatically runs `dotnet build` on your DAL project
2. **Loads entity via reflection** — Reads properties from the compiled assembly
3. **Filters audit fields** — Excludes `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`, `IsActive`, `DeletedBy`, `DeletedAt`, `Revision` from DTOs
4. **Generates code** — Creates all selected component files in their correct locations

## ⚠️ Important Notes

- **Validators are only generated for AddDto & UpdateDto** — The base `EntityDto` does not get a validator since it's used for read operations.
- **Navigation properties and collections are excluded** — Only value types, strings, and Guids are included in DTOs.
- **`//Generated entities` comment is required** — The `DbSet` generator looks for this comment in your DbContext to know where to insert. If missing, you'll need to add the DbSet manually.
- **DAL must build successfully** — The tool runs `dotnet build` on your DAL project before generation. If it fails, no code will be generated.

## 📋 Requirements

- A `*.sln` file in the project root
- Standard XFramework project structure: **DAL**, **BLL**, **API**, and **Dtos** projects
- A `//Generated entities` comment in your DbContext for automatic DbSet insertion

## 📄 License

MIT
