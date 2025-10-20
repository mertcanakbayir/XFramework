using System.Text.RegularExpressions;

namespace XFramework.Generator.Generators
{
    public class ContextGenerator
    {
        public void AddDbSet(Type entity, string contextPath)
        {
            var contextFile = Path.Combine(contextPath, "XFMContext.cs");

            if (!File.Exists(contextFile))
            {
                Console.WriteLine($"Context dosyası bulunamadı: {contextFile}");
                return;
            }

            var content = File.ReadAllText(contextFile);
            var entityName = entity.Name;
            var dbSetProperty = $"public DbSet<{entityName}> {entityName}s {{ get; set; }}";

            if (content.Contains($"DbSet<{entityName}>"))
            {
                Console.WriteLine($"DbSet<{entityName}> zaten mevcut.");
                return;
            }

            var newLine = content.Contains("\r\n") ? "\r\n" : "\n";

            var generatedEntitiesPattern = @"(//Generated entities)";
            var match = Regex.Match(content, generatedEntitiesPattern);

            if (match.Success)
            {
                var insertPosition = match.Index + match.Length;
                var newContent = content.Insert(insertPosition, $"{newLine}        {dbSetProperty}");

                File.WriteAllText(contextFile, newContent);
                Console.WriteLine($"✓ DbSet<{entityName}> Context'e eklendi.");
            }
            else
            {
                Console.WriteLine("⚠ '//Generated entities' yorumu bulunamadı. DbSet manuel olarak eklenmelidir.");
            }
        }
    }
}
