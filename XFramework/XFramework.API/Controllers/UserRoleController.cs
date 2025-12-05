using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos;
using XFramework.Helper.ViewModels;

namespace XFramework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleService _userRoleService;
        public UserRoleController(UserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost("assign")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> Assign(UserRoleAssignDto dto)
        {
            return await _userRoleService.AssignRolesAsync(dto);
        }
    }
}
