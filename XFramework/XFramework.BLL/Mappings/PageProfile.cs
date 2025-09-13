using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.Page;

namespace XFramework.BLL.Mappings
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<Page, PageDto>().ReverseMap();

            CreateMap<PageAddDto, Page>().ReverseMap();

            CreateMap<PageUpdateDto, Page>().ReverseMap();
        }
    }
}
