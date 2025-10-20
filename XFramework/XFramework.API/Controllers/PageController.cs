using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos.Page;
using XFramework.Helper.ViewModels;

namespace XFramework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly PageService _pageService;

        public PageController(PageService pageService)
        {
            _pageService = pageService;
        }
        [HttpPost]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> AddPage(PageAddDto pageAddDto)
        {

            return await _pageService.AddAsync(pageAddDto);
        }

        [HttpGet]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<List<PageDto>>> GetPagesByUser(int userId)
        {
            return await _pageService.GetAllAsync();
        }

        [HttpGet("parent")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<List<PageDto>>> GetByParentId(int parentId)
        {
            return await _pageService.GetAllAsync(e => e.ParentId == parentId);
        }


        [HttpGet("all")]
        public async Task<ResultViewModel<List<PageDto>>> GetPages()
        {
            return await _pageService.GetPagedAsync();
        }

        [HttpPut]
        public async Task<ResultViewModel<string>> UpdatePage(int id, PageUpdateDto pageUpdateDto)
        {
            return await _pageService.UpdateAsync(id, pageUpdateDto);
        }

    }
}
