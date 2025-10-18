namespace XFramework.Generator
{
    public class ValidationGenerator
    {
        public void Generate(Type entity, IEnumerable<string> dtoNames, string outputPath)
        {
            Directory.CreateDirectory(outputPath);

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
                    var filePath = Path.Combine(outputPath, $"{dtoName}Validator.cs");
                    File.WriteAllText(filePath, validator);
                }
            }
        }
    }
}
