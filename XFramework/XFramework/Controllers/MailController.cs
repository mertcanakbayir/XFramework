using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Services.MailService;
using XFramework.Dtos;

namespace XFramework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly MailService _mailService;

        public MailController(MailService mailService)
        {
            _mailService = mailService; 
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromBody] MailDto request)
        {
            try
            {
                await _mailService.SendEmailAsync(request.To, request.Subject, request.Body);
                return Ok(new { message = "Mail gönderildi" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
