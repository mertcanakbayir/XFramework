using AutoMapper;
using FluentValidation;
using XFramework.BLL.Services.Abstracts;
using XFramework.DAL.Entities;
using XFramework.Dtos.SystemSetting;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class SystemSettingService : BaseService<SystemSetting, SystemSettingDto, SystemSettingAddDto, SystemSettingUpdateDto>, IRegister
    {
        public SystemSettingService(IValidator<SystemSettingAddDto> addDtoValidator, IMapper mapper, IBaseRepository<SystemSetting> baseRepository, IUnitOfWork unitOfWork, IValidator<SystemSettingUpdateDto> updateDtoValidator) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
        }
    }
}
