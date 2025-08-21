using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Result;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos;

namespace XFramework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndpointController : ControllerBase
    {
        private readonly EndpointService _endpointService;

        public EndpointController(EndpointService endpointService)
        {
            _endpointService = endpointService;
        }

        [HttpGet]
        public async Task<ResultViewModel<List<EndpointDto>>> GetEndpointsByUser(int userId)
        {
            return await _endpointService.GetEndpointsByUser(userId);
        }

        [HttpPost]
        public async Task<ResultViewModel<string>> AddEndpoint(EndpointAddDto endpointAddDto)
        {
            return await _endpointService.AddEndpoint(endpointAddDto);
        }
    }
}
