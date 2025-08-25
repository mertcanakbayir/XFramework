using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Result;
using XFramework.BLL.Services.Concretes;
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
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> SendMail([FromBody] MailDto request)
        {
            return await _mailService.SendEmailAsync(request.To, request.Subject, request.Body, 3);
        }

        [HttpPost("send-mq")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> SendMailMQ([FromBody] MailDto request)
        {
            return await _mailService.SendEmailAsync(request.To, request.Subject, request.Body, 3, isQueue: true);
        }
    }
}
