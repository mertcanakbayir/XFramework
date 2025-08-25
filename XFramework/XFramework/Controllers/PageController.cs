using System.Security.Claims;
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
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> AddPage(PageAddDto pageAddDto)
        {
            var subClaim = User?.FindFirst(ClaimTypes.NameIdentifier);
            if (subClaim == null)
                return ResultViewModel<string>.Failure("User not authorized");

            var userId = int.Parse(subClaim.Value);
            return await _pageService.AddPage(pageAddDto, userId);
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
