using AutoMapper;
using MyApp.DAL.Entities;
using MyApp.Dtos.SystemSetting;

namespace MyApp.BLL.Mappings
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
