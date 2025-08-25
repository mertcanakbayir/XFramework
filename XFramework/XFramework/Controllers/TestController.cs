using Microsoft.AspNetCore.Mvc;

namespace XFramework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
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
    }
}
