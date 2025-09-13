using System.Reflection;

namespace DtoMapperGenerator
{
    public class DtoGenerator
    {

        private readonly HashSet<string> _auditFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
          "CreatedAt","CreatedBy","UpdatedAt","UpdatedBy","IsActive","DeletedBy","DeletedAt","Revision"
        };
        public void Generate(Type entity, string outputPath)
        {
            var folderPath = Path.Combine(outputPath, entity.Name);

            Directory.CreateDirectory(folderPath);

            var props = entity.GetProperties()
            .Where(p => p.PropertyType == typeof(string)
                    || p.PropertyType.IsValueType
                    || p.PropertyType == typeof(Guid))
            .Where(p => !typeof(System.Collections.ICollection).IsAssignableFrom(p.PropertyType))
            .Where(p => !_auditFields.Contains(p.Name))
            .Where(p => p.PropertyType.Namespace != "XFramework.DAL.Entities")
            .ToList();


            var commonProps = props.Where(p => !_auditFields.Contains(p.Name)).ToList();

            WriteDto(folderPath, entity.Name + "Dto", commonProps, entity.Name);

            var addProps = commonProps
              .Where(p => !string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase))
              .ToList();
            WriteDto(folderPath, entity.Name + "AddDto", addProps, entity.Name);
            var updateProps = commonProps.ToList();
            var revisionProperty = entity.GetProperties()
                .FirstOrDefault(p => string.Equals(p.Name, "Revision", StringComparison.OrdinalIgnoreCase));

            if (revisionProperty != null && !updateProps.Contains(revisionProperty))
            {
                updateProps.Add(revisionProperty);
            }
            WriteDto(folderPath, entity.Name + "UpdateDto", updateProps, entity.Name);

        }



        private void WriteDto(string folderPath, string dtoName, List<PropertyInfo> props, string entityName)
        {
            var properties = string.Join(Environment.NewLine,
                props.Select(p => $"        public {GetTypeName(p.PropertyType)} {p.Name} {{ get; set; }}"));

            var dto = $@"
namespace XFramework.Dtos.{entityName}
{{
    public class {dtoName}
    {{
{properties}
    }}
}}";
            File.WriteAllText(Path.Combine(folderPath, $"{dtoName}.cs"), dto);
        }

        private string GetTypeName(Type type)
        {
            return type.Name switch
            {
                "Int32" => "int",
                "Int64" => "long",
                "Boolean" => "bool",
                "String" => "string",
                "Decimal" => "decimal",
                "DateTime" => "DateTime",
                "Guid" => "Guid",
                _ => type.Name
            };
        }
    }
}

