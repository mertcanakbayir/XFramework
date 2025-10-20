using System.Reflection;
using XFramework.Generator;
using XFramework.Generator.Utils;

Console.WriteLine("Lütfen Entity adını girin: \n(Boş bırakırsanız tüm Entities klasörünü tarar ve tüm entityler için dto-mapper oluşturur)");
var entityName = Console.ReadLine();

var solutionRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.."));

var dtoOutput = Path.Combine(solutionRoot, "XFramework.Dtos");
var mapperOutput = Path.Combine(solutionRoot, "XFramework.BLL", "Mappings");
var validatorOutput = Path.Combine(solutionRoot, "XFramework.BLL", "Utilities", "ValidationRulers");
var serviceOutput = Path.Combine(solutionRoot, "XFramework.BLL", "Services", "Concretes");
var controllerOutput = Path.Combine(solutionRoot, "XFramework.API", "Controllers");
var dtoGenerator = new DtoGenerator();
var mapperGenerator = new MapperGenerator();
var validationGenerator = new ValidationGenerator();
var serviceGenerator = new ServiceGenerator();
var controllerGenerator = new ControllerGenerator();
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
}
else
{
    Console.WriteLine($"Entity '{entityName}' bulunamadı.");
}