using AutoMapper;
using MyApp.DAL.Entities;
using MyApp.Dtos.Endpoint;

namespace MyApp.BLL.Mappings
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
