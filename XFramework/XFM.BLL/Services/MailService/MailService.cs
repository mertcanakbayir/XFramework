using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using XFM.BLL.Result;
using XFM.DAL;
using XFM.DAL.Abstract;
using XFramework.BLL.Services.RabbitMQService;
using XFramework.BLL.Utilities;
using XFramework.BLL.Utilities.ValidationRulers;
using XFramework.DAL.Entities;
using XFramework.Dtos;

namespace XFramework.BLL.Services.MailService
{
    public class MailService
    {
        private readonly EncryptionHelper _crypto;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<SystemSetting> _systemSettingRepository;
        private readonly MailQueueService _MailQueueService;

        public MailService(XFMContext context, EncryptionHelper crypto, IBaseRepository<SystemSetting> systemSettingRepository, IMapper mapper, MailQueueService mailQueueService)
        {
            _systemSettingRepository = systemSettingRepository;
            _crypto = crypto;
            _mapper = mapper;
            _MailQueueService = mailQueueService;
        }

        public async Task<ResultViewModel<string>> SendEmailAsync(string to, string subject, string body, int settingId)
        {
            var mailSettings = await _systemSettingRepository.GetAsync(e => e.Id == settingId, includeFunc: query => query.Include(e => e.SystemSettingDetails));
            if (mailSettings == null)
                throw new Exception("Mail ayarları bulunamadı");

            var details = mailSettings.SystemSettingDetails.ToDictionary(d => d.Key, d => d.Value);
            var dto = _mapper.Map<MailSettingDto>(details);
            var validator = new MailSettingsDtoValidator();
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultViewModel<string>.Failure("Mail ayarları doğrulama hatası!", errors, 400);
            }
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(dto.SenderEmail, dto.SenderEmail));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(dto.SmtpHost, dto.SmtpPort, dto.EnableSsl);
            //await client.AuthenticateAsync(settings.SmtpUser, password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return ResultViewModel<string>.Success("Mail başarıyla gönderildi.", 200);
        }

        public async Task<ResultViewModel<string>> SendEmailAsync(string to, string subject, string body, int settingId, bool isQueue)
        {
            if (!isQueue)
                throw new ArgumentException("isQueue true olmalı, SMTP için diğer overload kullanın.");

            try
            {
                await _MailQueueService.EnqueueMailAsync(to, subject, body);
                return ResultViewModel<string>.Success("Mail RabbitMQ kuyruğuna eklendi.", 200);
            }
            catch (Exception ex)
            {
                return ResultViewModel<string>.Failure(
                    "Mail RabbitMQ kuyruğuna eklenirken hata oluştu: " + ex.Message, null, 500);
            }
        }



    }
}
