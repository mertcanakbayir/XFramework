using FluentValidation;
using XFramework.Dtos;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(e => e.Email).EmailAddress().WithMessage("Please enter a valid email address.")
                .NotEmpty().WithMessage("Email is required");
        }
    }
}
