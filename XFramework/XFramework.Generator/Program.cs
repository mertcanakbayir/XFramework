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

var solutionRoot = FindSolutionRoot();
if (solutionRoot == null)
{
    Console.WriteLine("Solution (.sln) not found");
    return;
}
var projectName = Path.GetFileNameWithoutExtension(
    Directory.GetFiles(solutionRoot, "*.sln").First()
);

var dalPath = FindProject(solutionRoot, "DAL");
var bllPath = FindProject(solutionRoot, "BLL");
var apiPath = FindProject(solutionRoot, "API");
var dtosPath = FindProject(solutionRoot, "Dtos", "DTOs", "Dto");

if (dalPath == null || bllPath == null || apiPath == null || dtosPath == null)
{
    Console.WriteLine("Required projects not found (DAL, BLL, API, Dtos)");
    Console.WriteLine($"{dalPath}");
    Console.WriteLine($"{bllPath}");
    Console.WriteLine($"{apiPath}");
    Console.WriteLine($"{dtosPath}");
    return;
}

var dtoOutput = dtosPath;
var mapperOutput = Path.Combine(bllPath, "Mappings");
var validatorOutput = Path.Combine(bllPath, "Utilities", "ValidationRulers");
var serviceOutput = Path.Combine(bllPath, "Services", "Concretes");
var controllerOutput = Path.Combine(apiPath, "Controllers");


// Generator instances
var dtoGenerator = new DtoGenerator();
var mapperGenerator = new MapperGenerator();
var validationGenerator = new ValidationGenerator();
var serviceGenerator = new ServiceGenerator();
var controllerGenerator = new ControllerGenerator();
var contextGenerator = new ContextGenerator();

Console.WriteLine($" Generating code for entity: {entityName}");
if (skipComponents.Any())
{
    Console.WriteLine($"  Skipping: {string.Join(", ", skipComponents)}");
}


var dalCsproj = Directory.GetFiles(dalPath, "*.csproj").First();

DALBuilder.Build(dalCsproj);

var dalDllPath = Directory
    .GetFiles(Path.Combine(dalPath, "bin"), "*.dll", SearchOption.AllDirectories)
    .FirstOrDefault(f => f.Contains("DAL", StringComparison.OrdinalIgnoreCase));

if (dalDllPath == null)
{
    Console.WriteLine(" DAL assembly not found");
    return;
}

// 4) Assembly’i yükle
var asm = Assembly.LoadFrom(dalDllPath);
var entity = asm.GetTypes().FirstOrDefault(t => t.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));

if (entity != null)
{
    string[] dtoNames = Array.Empty<string>();

    if (!skipComponents.Contains("Dto"))
    {
        Console.WriteLine("  Generating DTOs...");
        dtoNames = dtoGenerator.Generate(entity, projectName, dtoOutput).ToArray();
    }

    if (!skipComponents.Contains("Mapper"))
    {
        Console.WriteLine("  Generating Mapper...");
        mapperGenerator.Generate(new[] { entity }, projectName, mapperOutput);
    }

    if (!skipComponents.Contains("Validator") && !skipComponents.Contains("Validation"))
    {
        Console.WriteLine("  Generating Validator...");
        validationGenerator.Generate(entity, projectName, dtoNames, validatorOutput);
    }

    if (!skipComponents.Contains("Service"))
    {
        Console.WriteLine("  Generating Service...");
        serviceGenerator.Generate(new[] { entity }, projectName, dtoNames, serviceOutput);
    }

    if (!skipComponents.Contains("Controller"))
    {
        Console.WriteLine("  Generating Controller...");
        controllerGenerator.Generate(entity, projectName, controllerOutput);
    }

    if (!skipComponents.Contains("DbSet") && !skipComponents.Contains("Context"))
    {
        Console.WriteLine("  Adding DbSet to Context...");
        contextGenerator.AddDbSet(entity, projectName, dalPath);
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

static string? FindProject(string solutionRoot, params string[] suffixes)
{
    var csprojs = Directory.GetFiles(solutionRoot, "*.csproj", SearchOption.AllDirectories);

    foreach (var csproj in csprojs)
    {
        var name = Path.GetFileNameWithoutExtension(csproj);

        foreach (var suffix in suffixes)
        {
            if (name.Contains(suffix, StringComparison.OrdinalIgnoreCase))
                return Path.GetDirectoryName(csproj);
        }
    }

    return null;
}