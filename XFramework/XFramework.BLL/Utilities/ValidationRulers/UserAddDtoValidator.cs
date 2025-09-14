using FluentValidation;
using XFramework.Dtos.User;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class UserAddDtoValidator : AbstractValidator<UserAddDto>
    {
        public UserAddDtoValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("E-mail boş olamaz.");
            RuleFor(e => e.Username).NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MinimumLength(5).WithMessage("Kullanıcı adı 5 karakterden kısa olamaz");
            RuleFor(e => e.Password).MinimumLength(6).WithMessage("Şifre 6 karakterden uzun olmalı.")
                .NotEmpty().WithMessage("Şifre boş olamaz");
        }
    }
}
