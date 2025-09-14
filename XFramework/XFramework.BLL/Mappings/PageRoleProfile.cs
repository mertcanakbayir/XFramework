
using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.PageRole;

namespace XFramework.BLL.Mappings
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
