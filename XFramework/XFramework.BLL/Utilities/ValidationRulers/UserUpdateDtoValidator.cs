using FluentValidation;
using XFramework.Dtos;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email address cannot be empty.")
                .EmailAddress().WithMessage("Please check the email address format.");

            RuleFor(e => e.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(6).WithMessage("Password must be at least six characters long.");
        }
    }
}
