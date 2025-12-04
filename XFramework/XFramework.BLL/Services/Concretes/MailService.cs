using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Serilog;
using XFramework.Helper.ViewModels;

namespace XFramework.BLL.Services.Concretes
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

        public async Task<ResultViewModel<string>> SendDirectMailAsync(string to, string subject, string body, int settingId)
        {
            try
            {
                var mailSettings = await _systemSettingDetailService.GetMailOptionsAsync(settingId);
                if (mailSettings == null)
                    throw new KeyNotFoundException("Mail settings not found.");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(mailSettings.SenderEmail, mailSettings.SenderEmail));
                message.To.Add(MailboxAddress.Parse(to));
                message.Subject = subject;

                message.Body = new TextPart("html") { Text = body };
                var socketOption = GetOption(mailSettings.SmtpPort);

                using var client = new SmtpClient();

                await client.ConnectAsync(mailSettings.SmtpHost, mailSettings.SmtpPort, socketOption);
                await client.AuthenticateAsync(mailSettings.SmtpUser, mailSettings.SmtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return ResultViewModel<string>.Success("Mail sended succesfully.", 200);
            }
            catch (SmtpCommandException ex)
            {
                Log.Error(ex, "SMTP command error while sending email.");
                return ResultViewModel<string>.Failure("SMTP command error.", new List<string> { ex.Message }, 500);
            }
            catch (SmtpProtocolException ex)
            {
                Log.Error(ex, "SMTP protocol error while sending email.");
                return ResultViewModel<string>.Failure("SMTP protocol error.", new List<string> { ex.Message }, 500);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unknown SMTP error.");
                return ResultViewModel<string>.Failure("Unknown SMTP error.", new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ResultViewModel<string>> SendQueueMailAsync(string to, string subject, string body, int settingId, bool isQueue)
        {
            if (!isQueue)
                throw new ArgumentException("isQueue must be true; use the other overload for SMTP.");

            try
            {
                await _MailQueueService.EnqueueMailAsync(to, subject, body);
                return ResultViewModel<string>.Success("Mail added to queue successfully.", 200);
            }
            catch (Exception ex)
            {
                return ResultViewModel<string>.Failure(
                    "An error occurred while adding mail to the queue:" + ex.Message, null, 500);
            }
        }
        private SecureSocketOptions GetOption(int port)
        {
            return port switch
            {
                465 => SecureSocketOptions.SslOnConnect,
                587 => SecureSocketOptions.StartTls,
                2525 => SecureSocketOptions.StartTls,
                25 => SecureSocketOptions.None,
                _ => SecureSocketOptions.StartTls
            };
        }
    }
}
