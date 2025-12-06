using Microsoft.AspNetCore.Mvc;
using MyApp.BLL.Services.Concretes;
using MyApp.Dtos;
using MyApp.Helper.ViewModels;

namespace MyApp.API.Controllers
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