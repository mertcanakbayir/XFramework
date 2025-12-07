using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MyApp.BLL.Services.Concretes;
using MyApp.Dtos.Endpoint;
using MyApp.Helper.ViewModels;

namespace MyApp.API.Controllers
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

            return await _endpointService.AddAsync(endpointAddDto);
        }

        [HttpPut]
        public async Task<ResultViewModel<string>> UpdateEndpoint(int endpointId, EndpointUpdateDto endpointUpdateDto)
        {
            return await _endpointService.UpdateAsync(endpointId, endpointUpdateDto);
        }

        [HttpDelete]
        public async Task<ResultViewModel<string>> DeleteEndpoint(int endpointId)
        {
            return await _endpointService.DeleteAsync(endpointId);
        }
    }
}
