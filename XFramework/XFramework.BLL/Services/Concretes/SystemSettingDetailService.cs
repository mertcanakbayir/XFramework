using AutoMapper;
using FluentValidation;
using XFramework.DAL.Entities;
using XFramework.Dtos.SystemSettingDetail;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class SystemSettingDetailService : BaseService<SystemSettingDetail, SystemSettingDetailDto, SystemSettingDetailAddDto, SystemSettingDetailUpdateDto>
    {
        public SystemSettingDetailService(IValidator<SystemSettingDetailAddDto> addDtoValidator, IMapper mapper, IBaseRepository<SystemSettingDetail> baseRepository, IUnitOfWork unitOfWork, IValidator<SystemSettingDetailUpdateDto> updateDtoValidator) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
        }
    }
}
