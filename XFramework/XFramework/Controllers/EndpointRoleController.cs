using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos.EndpointRole;
using XFramework.Helper.ViewModels;

namespace XFramework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndpointRoleController : ControllerBase
    {
        private readonly EndpointRoleService _endpointRoleService;
        public EndpointRoleController(EndpointRoleService endpointRoleService)
        {
            _endpointRoleService = endpointRoleService;
        }
        [HttpPost]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> AddEndpointRole(EndpointRoleAddDto endpointRoleAddDto)
        {
            return await _endpointRoleService.AddEndpointRole(endpointRoleAddDto);
        }

        [HttpGet("endpoint")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<List<EndpointRoleDto>>> GetEndpointRolesByEndpointId(int endpointId)
        {
            return await _endpointRoleService.GetPagedAsync(e => e.EndpointId == endpointId);
        }

        [HttpGet("role")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<List<EndpointRoleDto>>> GetEndpointRolesByRoleId(int roleId)
        {
            return await _endpointRoleService.GetPagedAsync(e => e.RoleId == roleId);
        }

        [HttpPut]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> UpdateEndpointRole(int id, EndpointRoleUpdateDto endpointRoleUpdateDto)
        {
            return await _endpointRoleService.UpdateAsync(id, endpointRoleUpdateDto);
        }
    }
}
