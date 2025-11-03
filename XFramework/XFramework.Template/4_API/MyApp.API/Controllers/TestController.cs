using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApp.BLL.Services.Concretes;
using MyApp.Helper.ViewModels;

namespace MyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly LogSettingsService _logSettingsService;
        public TestController(LogSettingsService logSettingsService)
        {
            _logSettingsService = logSettingsService;
        }
        [HttpGet("throw")]
        public IActionResult ThrowException()
        {
            throw new Exception("Bu bir test exception");
        }

        [HttpGet("ok")]
        public IActionResult OkTest()
        {
            return Ok(new { Message = "Her şey yolunda" });
        }

        [HttpPost("logSwitch")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<string> LogSwitch([FromQuery] bool logSwitch, [FromServices] LogSettingsService logSettingsService)
        {
            return await _logSettingsService.ToggleLogAsync(10, logSwitch);
        }
    }
}
