using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XFramework.Generator
{
    public class ServiceGenerator
    {
        public void Generate(IEnumerable<Type> entities, IEnumerable<string> dtoNames, string outputPath)
        {
            var dtoList = dtoNames.ToList();
            Directory.CreateDirectory(outputPath);
            foreach (var entity in entities)
            {

                var service = $@"
using AutoMapper;
using FluentValidation;
using XFramework.BLL.Services.Abstracts;
using XFramework.DAL.Entities;
using XFramework.Dtos.{entity.Name};
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
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
            }
        }
    }
}
