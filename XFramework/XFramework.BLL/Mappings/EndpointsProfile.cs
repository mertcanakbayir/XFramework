using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.Endpoint;

namespace XFramework.BLL.Mappings
{
    public class EndpointsProfile : Profile
    {
        public EndpointsProfile()
        {
            CreateMap<Endpoint, EndpointDto>();

            CreateMap<EndpointDto, Endpoint>();

            CreateMap<EndpointAddDto, Endpoint>();
        }
    }
}
