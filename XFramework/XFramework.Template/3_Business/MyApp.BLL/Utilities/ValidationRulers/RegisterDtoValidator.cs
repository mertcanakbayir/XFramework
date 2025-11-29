using Dtos;
using FluentValidation;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {

        public RegisterDtoValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Please enter a valid email format.")
                .MaximumLength(50).WithMessage("Email address cannot be longer than 50 characters.");

            RuleFor(e => e.Username)
                .NotEmpty().WithMessage("Username cannot be empty.")
                .MaximumLength(30).WithMessage("Username cannot be longer than 30 characters.");

            RuleFor(e => e.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        }
    }
}
