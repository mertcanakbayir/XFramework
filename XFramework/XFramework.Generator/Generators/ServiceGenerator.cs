namespace XFramework.Generator.Generators
{
    public class ServiceGenerator
    {
        public void Generate(IEnumerable<Type> entities, string projectName, IEnumerable<string> dtoNames, string outputPath)
        {
            var dtoList = dtoNames.ToList();
            Directory.CreateDirectory(outputPath);
            foreach (var entity in entities)
            {

                var service = $@"
using AutoMapper;
using FluentValidation;
using {projectName}.BLL.Services.Abstracts;
using {projectName}.DAL.Entities;
using {projectName}.Dtos.{entity.Name};
using {projectName}.Repository.Repositories.Abstract;

namespace {projectName}.BLL.Services.Concretes
{{
    public class {entity.Name}Service : BaseService<{entity.Name},{dtoList[0]},{dtoList[1]},{dtoList[2]}>,IRegister
    {{
    public {entity.Name}Service(IValidator<{dtoList[1]}> addDtoValidator, IMapper mapper, IBaseRepository<{entity.Name}> baseRepository, IUnitOfWork unitOfWork, IValidator<{dtoList[2]}> updateDtoValidator) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {{
           
        }}  
    }}
}}
";
                File.WriteAllText(Path.Combine(outputPath, $"{entity.Name}Service.cs"), service);
                Console.WriteLine($"{entity.Name}Service created.");
            }
        }
    }
}
