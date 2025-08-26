using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Result;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos;

namespace XFramework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<ResultViewModel<List<RoleDto>>> GetUserRoles(int userId)
        {
            return await _roleService.GetRolesByUser(userId);
        }

        [HttpPost]
        public async Task<ResultViewModel<string>> AddUserRole(RoleAddDto addRole)
        {
            return await _roleService.AddRole(addRole);
        }

        [HttpPost("addPageRole")]
        public async Task<ResultViewModel<string>> AddPageRole(PageRoleAddDto pageRoleAddDto)
        {
            return await _roleService.AddPageRole(pageRoleAddDto);
        }

        [HttpPost("addEndpointRole")]
        public async Task<ResultViewModel<string>> AddEndpointRole(EndpointRoleAddDto endpointRoleAddDto)
        {
            return await _roleService.AddEndpointRole(endpointRoleAddDto);
        }

    }
}
