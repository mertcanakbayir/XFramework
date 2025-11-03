using AutoMapper;
using FluentValidation;
using MyApp.BLL.Services.Abstracts;
using MyApp.DAL.Entities;
using MyApp.Dtos.SystemSetting;
using MyApp.Repository.Repositories.Abstract;

namespace MyApp.BLL.Services.Concretes
{
    public class SystemSettingService : BaseService<SystemSetting, SystemSettingDto, SystemSettingAddDto, SystemSettingUpdateDto>, IRegister
    {
        public SystemSettingService(IValidator<SystemSettingAddDto> addDtoValidator, IMapper mapper, IBaseRepository<SystemSetting> baseRepository, IUnitOfWork unitOfWork, IValidator<SystemSettingUpdateDto> updateDtoValidator) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
        }
    }
}
