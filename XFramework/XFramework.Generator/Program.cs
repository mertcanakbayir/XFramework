using System.Reflection;
using XFramework.Generator.Generators;
using XFramework.Generator.Utils;

// Parse command line arguments
var commandArgs = Environment.GetCommandLineArgs().Skip(1).ToArray();

if (commandArgs.Length == 0 || commandArgs.Contains("--help") || commandArgs.Contains("-h"))
{
    PrintHelp();
    return;
}

var entityName = commandArgs[0];
var skipComponents = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

// Parse skip flags
for (int i = 1; i < commandArgs.Length; i++)
{
    if (commandArgs[i].Equals("--skip", StringComparison.OrdinalIgnoreCase) && i + 1 < commandArgs.Length)
    {
        var components = commandArgs[i + 1].Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var component in components)
        {
            skipComponents.Add(component.Trim());
        }
        i++;
    }
}

var solutionRoot = FindSolutionRoot() ?? Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.."));
var contextPath = Path.Combine(solutionRoot, "XFramework.DAL");

// Outputs
var dtoOutput = Path.Combine(solutionRoot, "XFramework.Dtos");
var mapperOutput = Path.Combine(solutionRoot, "XFramework.BLL", "Mappings");
var validatorOutput = Path.Combine(solutionRoot, "XFramework.BLL", "Utilities", "ValidationRulers");
var serviceOutput = Path.Combine(solutionRoot, "XFramework.BLL", "Services", "Concretes");
var controllerOutput = Path.Combine(solutionRoot, "XFramework.API", "Controllers");

// Generator instances
var dtoGenerator = new DtoGenerator();
var mapperGenerator = new MapperGenerator();
var validationGenerator = new ValidationGenerator();
var serviceGenerator = new ServiceGenerator();
var controllerGenerator = new ControllerGenerator();
var contextGenerator = new ContextGenerator();

Console.WriteLine($" Generating code for entity: {entityName}");
Console.WriteLine($" Solution root: {solutionRoot}");

if (skipComponents.Any())
{
    Console.WriteLine($"  Skipping: {string.Join(", ", skipComponents)}");
}


DALBuilder.Build(solutionRoot);
var dalPath = Path.Combine(solutionRoot, "XFramework.DAL", "bin", "Debug", "net8.0", "XFramework.DAL.dll");
var asm = Assembly.LoadFrom(dalPath);
var entity = asm.GetTypes().FirstOrDefault(t => t.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));

if (entity != null)
{
    string[] dtoNames = Array.Empty<string>();

    if (!skipComponents.Contains("Dto"))
    {
        Console.WriteLine("  Generating DTOs...");
        dtoNames = dtoGenerator.Generate(entity, dtoOutput).ToArray();
    }

    if (!skipComponents.Contains("Mapper"))
    {
        Console.WriteLine("  Generating Mapper...");
        mapperGenerator.Generate(new[] { entity }, mapperOutput);
    }

    if (!skipComponents.Contains("Validator") && !skipComponents.Contains("Validation"))
    {
        Console.WriteLine("  Generating Validator...");
        validationGenerator.Generate(entity, dtoNames, validatorOutput);
    }

    if (!skipComponents.Contains("Service"))
    {
        Console.WriteLine("  Generating Service...");
        serviceGenerator.Generate(new[] { entity }, dtoNames, serviceOutput);
    }

    if (!skipComponents.Contains("Controller"))
    {
        Console.WriteLine("  Generating Controller...");
        controllerGenerator.Generate(entity, dtoNames, controllerOutput);
    }

    if (!skipComponents.Contains("DbSet") && !skipComponents.Contains("Context"))
    {
        Console.WriteLine("  Adding DbSet to Context...");
        contextGenerator.AddDbSet(entity, contextPath);
    }

    Console.WriteLine($"\n All files generated successfully for entity: {entityName}");
}
else
{
    Console.WriteLine($" Entity '{entityName}' not found.");
    Environment.Exit(1);
}


static void PrintHelp()
{
    Console.WriteLine(@"
XFramework Code Generator
=========================

Usage:
  xgen <EntityName> [options]
  dotnet xgen <EntityName> [options]

Options:
  --skip <components>    Skip specific components (comma-separated)
                        Available: Dto, Mapper, Validator, Service, Controller, DbSet

Examples:
  xgen Product
  xgen Product --skip Mapper
  xgen Product --skip Mapper,Validator
  dotnet xgen Order --skip Controller,DbSet

Help:
  --help, -h            Show this help message
");
}

static string? FindSolutionRoot()
{
    var currentDir = Directory.GetCurrentDirectory();

    while (currentDir != null)
    {
        if (Directory.GetFiles(currentDir, "*.sln").Any())
        {
            return currentDir;
        }
        currentDir = Directory.GetParent(currentDir)?.FullName;
    }

    return null;
}