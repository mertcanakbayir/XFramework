namespace DtoMapperGenerator
{
    public class DtoGenerator
    {


        public void Generate(Type entity, string outputPath)
        {
            Directory.CreateDirectory(outputPath);

            var props = entity.GetProperties()
            .Where(p => p.PropertyType == typeof(string) || !typeof(System.Collections.ICollection).IsAssignableFrom(p.PropertyType))
            .ToList();

            var properties = string.Join(Environment.NewLine,
                props.Select(p => $"        public {GetTypeName(p.PropertyType)} {p.Name} {{get; set;}}"));

            var dtoName = entity.Name + "Dto";

            var dto = $@"
namespace XFramework.Dtos
{{
    public class {dtoName}
    {{
        {properties}
    }}
}}
";
            File.WriteAllText(Path.Combine(outputPath, $"{dtoName}.cs"), dto);
        }

        string GetTypeName(Type type)
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
