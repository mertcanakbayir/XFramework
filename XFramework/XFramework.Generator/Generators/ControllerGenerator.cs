namespace XFramework.Generator.Generators
{
    public class ControllerGenerator
    {
        public void Generate(Type entity, IEnumerable<string> dtos, string outputPath)
        {
            Directory.CreateDirectory(outputPath);
            //var dtoList = dtos.ToList();
            var controller = $@"
using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos.{entity.Name};
using XFramework.Helper.ViewModels;

namespace XFramework.API.Controllers
{{
    [Route(""api/[controller]"")]
    [ApiController]
    public class {entity.Name}Controller:ControllerBase
    {{
        private readonly {entity.Name}Service _{entity.Name}Service;
        
        public {entity.Name}Controller({entity.Name}Service {entity.Name}Service)
        {{
            _{entity.Name}Service = {entity.Name}Service;
        }}

        [HttpPost]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> Add{entity.Name}({entity.Name}AddDto {entity.Name}AddDto)
        {{
            return await _{entity.Name}Service.AddAsync({entity.Name}AddDto);
        }}

        [HttpGet]
        public async Task<ResultViewModel<List<{entity.Name}Dto>>> Get{entity.Name}s()
        {{
            return await _{entity.Name}Service.GetPagedAsync();
        }}

        [HttpGet(""{{id}}"")]
        public async Task<ResultViewModel<{entity.Name}Dto>> GetById(int id)
        {{
            return await _{entity.Name}Service.GetAsync(id);
        }}
        [HttpPut(""{{id}}"")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> Update{entity.Name}(int id, {entity.Name}UpdateDto {entity.Name}UpdateDto)
        {{
            return await _{entity.Name}Service.UpdateAsync(id, {entity.Name}UpdateDto);
        }}

        [HttpDelete(""{{id}}"")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> Delete{entity.Name}(int id)
        {{
            return await _{entity.Name}Service.DeleteAsync(id);
        }}
    }}
}}
";
            File.WriteAllText(Path.Combine(outputPath, $"{entity.Name}Controller.cs"), controller);
            Console.WriteLine($"✓ {entity.Name}Controller created.");
        }
    }
}
