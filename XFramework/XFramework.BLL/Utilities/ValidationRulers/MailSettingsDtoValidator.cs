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
            .NotEmpty().WithMessage("SMTP host is required.");

            RuleFor(x => x.SmtpPort)
                .InclusiveBetween(1, 65535)
                .WithMessage("SMTP port must be between 1 and 65535.");

            RuleFor(x => x.SenderEmail)
                .NotEmpty().WithMessage("Sender email is required.")
                .EmailAddress().WithMessage("Sender mail must be valid.");

            RuleFor(x => x.SmtpUser)
                .NotEmpty()
                .When(x => !string.IsNullOrWhiteSpace(x.SmtpPassword))
                .WithMessage("SMTP username is required when SMTP password is provided.");

            RuleFor(x => x.SmtpPassword)
                .NotEmpty()
                .When(x => !string.IsNullOrWhiteSpace(x.SmtpUser))
                .WithMessage("SMTP password is required when SMTP username is provided.");
        }
    }
}
