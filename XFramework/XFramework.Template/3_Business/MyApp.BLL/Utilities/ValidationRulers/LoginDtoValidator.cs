using Dtos;
using FluentValidation;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email address is requireq.").EmailAddress().
                WithMessage("Please enter a valid e-mail adress");

            RuleFor(e => e.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(6).
                WithMessage("Password must be at least 6 characters long.");
        }
    }
}
