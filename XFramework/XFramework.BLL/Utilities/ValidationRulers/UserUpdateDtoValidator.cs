using FluentValidation;
using XFramework.Dtos.User;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Mail adresi boş olamaz.").EmailAddress().WithMessage("Email adresi formatını lütfen kontrol edin.");

            RuleFor(e => e.Password).NotEmpty().WithMessage("Şifre boş olamaz.").MinimumLength(6).WithMessage("Şifre en az altı karakter olmalı.");

        }
    }
}
