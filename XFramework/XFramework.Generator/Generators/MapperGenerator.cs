namespace XFramework.Generator.Generators
{
    public class MapperGenerator
    {
        public void Generate(IEnumerable<Type> entities, string projectName, string outputPath)
        {
            Directory.CreateDirectory(outputPath);

            foreach (var e in entities)
            {

                var tDtoMapping = string.Join(Environment.NewLine, entities.Select(e => $"CreateMap<{e.Name}, {e.Name}Dto>().ReverseMap();"));
                var tAddDtoMapping = string.Join(Environment.NewLine, entities.Select(e => $"CreateMap<{e.Name}, {e.Name}AddDto>().ReverseMap();"));
                var tUpdateDtoMapping = string.Join(Environment.NewLine, entities.Select(e => $"CreateMap<{e.Name}, {e.Name}UpdateDto>().ReverseMap();"));

                var validation = $@"
using AutoMapper;
using {projectName}.DAL.Entities;
using {projectName}.Dtos.{e.Name};

namespace {projectName}.BLL.Mappings
{{

public class {e.Name}Profile : Profile
{{
     public {e.Name}Profile()
    {{
{tDtoMapping}
{tAddDtoMapping}
{tUpdateDtoMapping}
    }}
}}
}}
";
                File.WriteAllText(Path.Combine(outputPath, $"{e.Name}Profile.cs"), validation);
                Console.WriteLine($"Mapper created.");
            }

        }
    }
}
