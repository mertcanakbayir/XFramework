using Dtos;
using FluentValidation;

namespace XFM.BLL.Utilities.ValidationRulers
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("E-Mail adresi boş olamaz.").EmailAddress().
                WithMessage("E-Posta adresi doğru formatta olmalıdır.");

            RuleFor(e => e.Password).NotEmpty().WithMessage("Şifre boş olamaz.").MinimumLength(6).
                WithMessage("Şifre 6 karakterden kısa olamaz.");
        }
    }
}
