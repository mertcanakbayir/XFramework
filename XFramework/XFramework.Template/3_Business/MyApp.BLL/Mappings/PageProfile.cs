using AutoMapper;
using MyApp.DAL.Entities;
using MyApp.Dtos.Page;

namespace MyApp.BLL.Mappings
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
