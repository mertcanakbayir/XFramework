using Dtos;
using FluentValidation;
using MyApp.Dtos;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class MailSettingsDtoValidator : AbstractValidator<MailSettingDto>
    {
        public MailSettingsDtoValidator()
        {
            RuleFor(x => x.SmtpHost)
           .NotEmpty().WithMessage("SMTP host is required.");

            RuleFor(x => x.SmtpPort)
                .InclusiveBetween(1, 65535)
                .WithMessage("SMTP port must be between 1 and 65535.");

            RuleFor(x => x.SenderEmail)
                .NotEmpty().WithMessage("Sender email is required.")
                .EmailAddress().WithMessage("Sender mail must be valid.");

            RuleFor(x => x.SmtpUser)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.EncryptedPassword))
                .WithMessage("If password is not null, SMTP username is required");

            //RuleFor(x => x.EncryptedPassword)
            //    .NotEmpty().When(x => !string.IsNullOrEmpty(x.SmtpUser))
            //    .WithMessage("SMTP şifre boş olamaz, kullanıcı adı girilmiş ise.");
        }
    }
}
