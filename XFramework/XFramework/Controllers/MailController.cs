using Microsoft.AspNetCore.Mvc;
using XFM.BLL.Result;
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
        [ValidateFilter]
        public async Task<ResultViewModel<string>> SendMail([FromBody] MailDto request)
        {
            return await _mailService.SendEmailAsync(request.To, request.Subject, request.Body, 3);
        }

        [HttpPost("send-mq")]
        [ValidateFilter]
        public async Task<ResultViewModel<string>> SendMailMQ([FromBody] MailDto request)
        {
            return await _mailService.SendEmailAsync(request.To, request.Subject, request.Body, 3, isQueue: true);
        }
    }
}
