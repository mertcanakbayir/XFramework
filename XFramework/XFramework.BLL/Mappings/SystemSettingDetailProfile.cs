using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.SystemSettingDetail;

namespace XFramework.BLL.Mappings
{
    public class SystemSettingDetailProfile : Profile
    {
        public SystemSettingDetailProfile()
        {
            CreateMap<SystemSettingDetail, SystemSettingDetailDto>();

            CreateMap<SystemSettingDetailUpdateDto, SystemSettingDetail>().ReverseMap();

            CreateMap<SystemSettingDetailAddDto, SystemSettingDetail>().ReverseMap();
        }
    }
}
