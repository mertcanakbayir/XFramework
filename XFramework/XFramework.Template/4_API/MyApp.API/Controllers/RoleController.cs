using Microsoft.AspNetCore.Mvc;
using MyApp.BLL.Services.Concretes;
using MyApp.Dtos.Role;
using MyApp.Helper.ViewModels;

namespace MyApp.API.Controllers
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
            return await _roleService.AddAsync(addRole);
        }
    }
}
