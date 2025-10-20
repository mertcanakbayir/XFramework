using System.Reflection;
using XFramework.Generator.Generators;
using XFramework.Generator.Utils;

Console.WriteLine("Please enter entity name:");
var entityName = Console.ReadLine();

var solutionRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.."));
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
DALBuilder.Build(solutionRoot);
var dalPath = Path.Combine(solutionRoot, "XFramework.DAL", "bin", "Debug", "net8.0", "XFramework.DAL.dll");
var asm = Assembly.LoadFrom(dalPath);
var entity = asm.GetTypes().FirstOrDefault(t => t.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));

if (entity != null)
{
    var dtoNames = dtoGenerator.Generate(entity, dtoOutput);
    mapperGenerator.Generate(new[] { entity }, mapperOutput);
    validationGenerator.Generate(entity, dtoNames, validatorOutput);
    serviceGenerator.Generate(new[] { entity }, dtoNames, serviceOutput);
    controllerGenerator.Generate(entity, dtoNames, controllerOutput);
    contextGenerator.AddDbSet(entity, contextPath);

    Console.WriteLine($"\n✓ All files generated for entity:{entityName}");
}
else
{
    Console.WriteLine($"Entity '{entityName}' bulunamadı.");
}