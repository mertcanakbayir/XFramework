using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos;

namespace XFramework.BLL.Mappings
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<Page, PageDto>();
            CreateMap<PageDto, Page>();

            CreateMap<PageAddDto, Page>();
        }
    }
}
