using Dtos;
using FluentValidation;
using XFramework.Dtos;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class MailSettingsDtoValidator : AbstractValidator<MailSettingDto>
    {
        public MailSettingsDtoValidator()
        {
            RuleFor(x => x.SmtpHost)
           .NotEmpty().WithMessage("SMTP host boş olamaz.");

            RuleFor(x => x.SmtpPort)
                .InclusiveBetween(1, 65535)
                .WithMessage("SMTP port 1 ile 65535 arasında olmalıdır.");

            RuleFor(x => x.SenderEmail)
                .NotEmpty().WithMessage("Gönderici e-posta boş olamaz.")
                .EmailAddress().WithMessage("Gönderici e-posta geçerli bir e-posta olmalıdır.");

            RuleFor(x => x.SmtpUser)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.EncryptedPassword))
                .WithMessage("SMTP kullanıcı adı boş olamaz, şifre girilmiş ise.");

            //RuleFor(x => x.EncryptedPassword)
            //    .NotEmpty().When(x => !string.IsNullOrEmpty(x.SmtpUser))
            //    .WithMessage("SMTP şifre boş olamaz, kullanıcı adı girilmiş ise.");
        }
    }
}
