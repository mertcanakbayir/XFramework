using MailKit.Net.Smtp;
using MimeKit;
using XFM.DAL;
using XFM.DAL.Abstract;
using XFramework.BLL.Utilities;
using XFramework.DAL.Entities;

namespace XFramework.BLL.Services.MailService
{
    public class MailService
    {
        private readonly EncryptionHelper _crypto;
        private readonly IBaseRepository<MailSetting> _settingRepository;

        public MailService(XFMContext context,EncryptionHelper crypto, IBaseRepository<MailSetting> settingRepository)
        {
            _settingRepository = settingRepository;
            _crypto = crypto;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var settings = await _settingRepository.GetAsync(e=>e.Id==2);
            if (settings == null)
                throw new Exception("Mail ayarları bulunamadı");
            var password = _crypto.Decrypt(settings.EncryptedPassword);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(settings.SenderEmail, settings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(settings.SmtpHost, settings.SmtpPort, settings.EnableSsl);
            //await client.AuthenticateAsync(settings.SmtpUser, password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
    