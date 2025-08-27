using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos;

namespace XFramework.BLL.Mappings
{
    public class SystemSettingDetailProfile : Profile
    {
        public SystemSettingDetailProfile()
        {
            CreateMap<SystemSettingDetail, SystemSettingDetailDto>();
        }
    }
}
