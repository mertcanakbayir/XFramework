using System.Reflection;
using DtoMapperGenerator;

Console.WriteLine("Lütfen Entity adını girin: \n(Boş bırakırsanız tüm Entities klasörünü tarar ve tüm entityler için dto-mapper oluşturur)");
var entityName = Console.ReadLine();
var solutionRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.."));
var dtoOutput = Path.Combine(solutionRoot, "Dtos");
var mapperOutput = Path.Combine(solutionRoot, "XFramework.BLL", "Mappings");
var dtoGenerator = new DtoGenerator();
var mapperGenerator = new MapperGenerator();
var asm = Assembly.Load("XFramework.DAL");
var entity = asm.GetTypes().FirstOrDefault(t => t.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));

if (entity != null)
{
    dtoGenerator.Generate(entity, dtoOutput);
    Console.WriteLine($"{entityName} için DTO oluşturuldu!");
    mapperGenerator.Generate(new[] { entity }, mapperOutput);
    Console.WriteLine($"DTO Output Path: {dtoOutput}");
}
else
{
    Console.WriteLine($"Entity '{entityName}' bulunamadı.");
}