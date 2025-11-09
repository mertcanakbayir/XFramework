using FluentValidation;
using MyApp.Dtos;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Email format must be valid.");

            RuleFor(e => e.NewPassword)
                .Equal(x => x.ConfirmPassword).WithMessage("Passwords must match.");

        }
    }
}
