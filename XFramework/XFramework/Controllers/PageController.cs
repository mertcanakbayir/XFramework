using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos;
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

            return await _pageService.AddPage(pageAddDto);
        }

        [HttpGet]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<List<PageDto>>> GetPagesByUser(int userId)
        {
            return await _pageService.GetPagesByUser(userId);
        }

        [HttpGet("all")]
        public async Task<ResultViewModel<List<PageDto>>> GetPages()
        {
            return await _pageService.GetAllPages();
        }

        [HttpPut]
        public async Task<ResultViewModel<string>> UpdatePage(PageAddDto pageAddDto)
        {
            return await _pageService.UpdatePage(pageAddDto);
        }

    }
}
