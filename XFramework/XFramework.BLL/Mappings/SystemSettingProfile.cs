using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.SystemSetting;

namespace XFramework.BLL.Mappings
{
    public class SystemSettingProfile : Profile
    {
        public SystemSettingProfile()
        {
            CreateMap<SystemSettingDto, SystemSetting>();

            CreateMap<SystemSetting, SystemSettingDto>();
        }
    }
}
