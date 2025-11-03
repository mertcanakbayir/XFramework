
using AutoMapper;
using MyApp.DAL.Entities;
using MyApp.Dtos.PageRole;

namespace MyApp.BLL.Mappings
{

    public class PageRoleProfile : Profile
    {
        public PageRoleProfile()
        {
            CreateMap<PageRole, PageRoleDto>().ReverseMap();
            CreateMap<PageRoleAddDto, PageRole>().ReverseMap();
            CreateMap<PageRoleUpdateDto, PageRole>().ReverseMap();
        }
    }
}
