using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos;

namespace XFramework.BLL.Mappings
{
    public class EndpointsProfile : Profile
    {
        public EndpointsProfile()
        {
            CreateMap<Endpoint, EndpointDto>();

            CreateMap<EndpointDto, Endpoint>();
        }
    }
}
