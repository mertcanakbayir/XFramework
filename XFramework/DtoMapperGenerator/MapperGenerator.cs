namespace DtoMapperGenerator
{
    public class MapperGenerator
    {
        public void Generate(IEnumerable<Type> entities, string outputPath)
        {
            Directory.CreateDirectory(outputPath);

            foreach (var e in entities)
            {

                var mappings = string.Join(Environment.NewLine, entities.Select(e => $"CreateMap<{e.Name}, {e.Name}Dto>().ReverseMap();"));
                var profile = $@"
using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos;

namespace XFramework.BLL.Mappings
{{

public class {e.Name}Profile : Profile
{{
     public {e.Name}Profile()
    {{
{mappings}
    }}
}}
}}
";
                File.WriteAllText(Path.Combine(outputPath, $"{e.Name}Profile.cs"), profile);
            }

        }
    }
}
