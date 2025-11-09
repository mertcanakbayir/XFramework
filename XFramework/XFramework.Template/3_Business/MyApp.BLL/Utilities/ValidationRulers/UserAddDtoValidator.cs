using FluentValidation;
using MyApp.Dtos.User;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class UserAddDtoValidator : AbstractValidator<UserAddDto>
    {
        public UserAddDtoValidator()
        {
            RuleFor(e => e.Email)
      .NotEmpty().WithMessage("Email cannot be empty.");

            RuleFor(e => e.Username)
                .NotEmpty().WithMessage("Username cannot be empty.")
                .MinimumLength(5).WithMessage("Username cannot be shorter than 5 characters.");

            RuleFor(e => e.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        }
    }
}
