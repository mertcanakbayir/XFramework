using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos.EndpointRole;
using XFramework.Dtos.PageRole;
using XFramework.Helper.ViewModels;

namespace XFramework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageRoleController : ControllerBase
    {
        private readonly PageRoleService _pageRoleService;
        public PageRoleController(PageRoleService pageRoleService)
        {
            _pageRoleService = pageRoleService;
        }
        [HttpPost]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> AddPageRole(PageRoleAddDto pageRoleAddDto)
        {
            return await _pageRoleService.AddPageRole(pageRoleAddDto);
        }

        [HttpGet("page")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<List<PageRoleDto>>> GetPageRolesByPageId(int pageId)
        {
            return await _pageRoleService.GetPagedAsync(e => e.PageId == pageId);
        }

        [HttpGet("role")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<List<PageRoleDto>>> GetPageRolesByRoleId(int roleId)
        {
            return await _pageRoleService.GetPagedAsync(e => e.RoleId == roleId);
        }

        [HttpPut]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> UpdatePageRole(int id, PageRoleUpdateDto pageRoleUpdateDto)
        {
            return await _pageRoleService.UpdateAsync(id, pageRoleUpdateDto);
        }

    }
}
