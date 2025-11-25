using System.Reflection;

namespace XFramework.Generator.Generators
{
    public class DtoGenerator
    {
        private readonly HashSet<string> _auditFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
          "CreatedAt","CreatedBy","UpdatedAt","UpdatedBy","IsActive","DeletedBy","DeletedAt","Revision"
        };
        public IEnumerable<string> Generate(Type entity, string projectName, string outputPath)
        {
            var folderPath = Path.Combine(outputPath, entity.Name);

            Directory.CreateDirectory(folderPath);

            var props = entity.GetProperties()
            .Where(p => p.PropertyType == typeof(string)
                    || p.PropertyType.IsValueType
                    || p.PropertyType == typeof(Guid))
            .Where(p => !typeof(System.Collections.ICollection).IsAssignableFrom(p.PropertyType))
            .Where(p => !_auditFields.Contains(p.Name))
            .Where(p => p.PropertyType.Namespace != $"{projectName}.DAL.Entities")
            .ToList();

            var dtos = new List<string>();

            var commonProps = props.Where(p => !_auditFields.Contains(p.Name)).ToList();
            dtos.Add(entity.Name + "Dto");
            WriteDto(folderPath, projectName, entity.Name + "Dto", commonProps, entity.Name);

            var addProps = commonProps
              .Where(p => !string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase))
              .ToList();
            dtos.Add(entity.Name + "AddDto");
            WriteDto(folderPath, projectName, entity.Name + "AddDto", addProps, entity.Name);

            var updateProps = commonProps.Where(p => !string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase)).ToList();

            var revisionProperty = entity.GetProperties()
                .FirstOrDefault(p => string.Equals(p.Name, "Revision", StringComparison.OrdinalIgnoreCase));

            if (revisionProperty != null && !updateProps.Contains(revisionProperty))
            {
                updateProps.Add(revisionProperty);
            }
            dtos.Add(entity.Name + "UpdateDto");
            WriteDto(folderPath, projectName, entity.Name + "UpdateDto", updateProps, entity.Name);
            return dtos;
        }
        private void WriteDto(string folderPath, string projectName, string dtoName, List<PropertyInfo> props, string entityName)
        {
            var properties = string.Join(Environment.NewLine,
                props.Select(p => $"        public {GetTypeName(p.PropertyType)} {p.Name} {{ get; set; }}"));

            var dto = $@"
namespace {projectName}.Dtos.{entityName}
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
