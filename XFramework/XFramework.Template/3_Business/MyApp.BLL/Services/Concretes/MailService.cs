using AutoMapper;
using MailKit.Net.Smtp;
using MimeKit;
using MyApp.DAL;
using MyApp.Helper.Helpers;
using MyApp.Helper.ViewModels;

namespace MyApp.BLL.Services.Concretes
{
    public class MailService
    {
        private readonly IMapper _mapper;
        private readonly MailQueueService _MailQueueService;
        private readonly SystemSettingDetailService _systemSettingDetailService;

        public MailService(IMapper mapper, MailQueueService mailQueueService, SystemSettingDetailService systemSettingDetailService)
        {
            _mapper = mapper;
            _MailQueueService = mailQueueService;
            _systemSettingDetailService = systemSettingDetailService;
        }

        public async Task<ResultViewModel<string>> SendEmailAsync(string to, string subject, string body, int settingId)
        {
            var mailSettings = await _systemSettingDetailService.GetMailOptionsAsync(settingId);
            if (mailSettings == null)
                throw new Exception("Mail settings not found.");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailSettings.SenderEmail, mailSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(mailSettings.SmtpHost, mailSettings.SmtpPort, mailSettings.EnableSsl);
            //await client.AuthenticateAsync(settings.SmtpUser, password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return ResultViewModel<string>.Success("Mail sended succesfully.", 200);
        }

        public async Task<ResultViewModel<string>> SendEmailAsync(string to, string subject, string body, int settingId, bool isQueue)
        {
            if (!isQueue)
                throw new ArgumentException("isQueue must be true; use the other overload for SMTP.");

            try
            {
                await _MailQueueService.EnqueueMailAsync(to, subject, body);
                return ResultViewModel<string>.Success("Mail added to RabbitMQ queue successfully.", 200);
            }
            catch (Exception ex)
            {
                return ResultViewModel<string>.Failure(
                    "An error occurred while adding mail to the RabbitMQ queue:" + ex.Message, null, 500);
            }
        }
    }
}
