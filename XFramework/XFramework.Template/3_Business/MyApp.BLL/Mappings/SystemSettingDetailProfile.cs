using AutoMapper;
using MyApp.DAL.Entities;
using MyApp.Dtos.SystemSettingDetail;

namespace MyApp.BLL.Mappings
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
