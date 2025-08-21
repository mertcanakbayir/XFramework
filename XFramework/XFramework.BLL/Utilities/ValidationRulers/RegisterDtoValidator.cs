using Dtos;
using FluentValidation;

namespace XFM.BLL.Utilities.ValidationRulers
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {

        public RegisterDtoValidator()
        {
            RuleFor(e=>e.Email).NotEmpty().WithMessage("E-Posta adresi girmek zorunldur.").EmailAddress().WithMessage("Geçerli bir e-posta formatı girin.").MaximumLength(50).WithMessage("E-Posta adresi 50 karakterden uzun olamaz.");
            RuleFor(e => e.Username).NotEmpty().WithMessage("Kullanıcı adı boş olamaz.").MaximumLength(30).WithMessage("Kullanıcı adresi 30 karakterden uzun olamaz.");
            RuleFor(e => e.Password).NotEmpty().WithMessage("Şifre boş olamaz.").MinimumLength(6).WithMessage("Şifre 6 karakterden kısa olamaz.");    
        }
    }
}
