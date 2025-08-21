using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Result;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos;

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
        [ValidateFilter]
        public async Task<ResultViewModel<string>> AddPage(PageAddDto pageAddDto)
        {
            return await _pageService.AddPage(pageAddDto);
        }

        [HttpGet]
        [ValidateFilter]
        public async Task<ResultViewModel<List<PageDto>>> GetPagesByUser(int userId)
        {
            return await _pageService.GetPagesByUser(userId);
        }

    }
}
