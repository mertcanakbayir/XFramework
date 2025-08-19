using FluentValidation;
using XFramework.Dtos;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("E-Mail boş olamaz.").EmailAddress().WithMessage("Mail adresi formatı doğru olmalıdır.");

            RuleFor(e => e.NewPassword).Equal(x => x.ConfirmPassword).WithMessage("Şifreler uyuşmalı");
        }
    }
}
