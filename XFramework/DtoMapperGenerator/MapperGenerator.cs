namespace DtoMapperGenerator
{
    public class MapperGenerator
    {
        public void Generate(IEnumerable<Type> entities, string outputPath)
        {
            Directory.CreateDirectory(outputPath);

            foreach (var e in entities)
            {

                var tDtoMapping = string.Join(Environment.NewLine, entities.Select(e => $"CreateMap<{e.Name}, {e.Name}Dto>().ReverseMap();"));
                var tAddDtoMapping = string.Join(Environment.NewLine, entities.Select(e => $"CreateMap<{e.Name}, {e.Name}AddDto>().ReverseMap();"));
                var tUpdateDtoMapping = string.Join(Environment.NewLine, entities.Select(e => $"CreateMap<{e.Name}, {e.Name}UpdateDto>().ReverseMap();"));

                var profile = $@"
using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.{e.Name};

namespace XFramework.BLL.Mappings
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
                File.WriteAllText(Path.Combine(outputPath, $"{e.Name}Profile.cs"), profile);
            }

        }
    }
}
