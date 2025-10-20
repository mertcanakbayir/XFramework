namespace XFramework.Generator.Generators
{
    public class ValidationGenerator
    {
        public void Generate(Type entity, IEnumerable<string> dtoNames, string outputPath)
        {
            var entityFolderPath = Path.Combine(outputPath, entity.Name);
            Directory.CreateDirectory(entityFolderPath);

            foreach (var dtoName in dtoNames)
            {
                if (dtoName == entity.Name + "Dto")
                {
                    continue;
                }
                else
                {
                    var validator = $@"
using FluentValidation;
using XFramework.Dtos.{entity.Name};

namespace XFramework.BLL.Utilities.ValidationRulers
{{
    public class {dtoName}Validator:AbstractValidator<{dtoName}>
{{
    public {dtoName}Validator()
        {{
          
        }}
    }}
}}
";

                    var filePath = Path.Combine(entityFolderPath, $"{dtoName}Validator.cs");
                    File.WriteAllText(filePath, validator);
                    Console.WriteLine($"✓ {dtoName}Validator created: {entity.Name}/{dtoName}Validator.cs");

                }
            }
        }
    }
}
