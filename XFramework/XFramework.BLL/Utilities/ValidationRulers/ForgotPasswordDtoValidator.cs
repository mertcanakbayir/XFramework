using FluentValidation;
using XFramework.Dtos;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(e => e.Email).EmailAddress().WithMessage("E-Posta formatı doğru olmalı.")
                .NotEmpty().WithMessage("E-Posta adresi boş olamaz.");
        }
    }
}
