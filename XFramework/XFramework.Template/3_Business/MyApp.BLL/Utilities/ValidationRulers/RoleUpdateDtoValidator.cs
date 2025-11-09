using FluentValidation;
using MyApp.Dtos.Role;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class RoleUpdateDtoValidator : AbstractValidator<RoleUpdateDto>
    {
        public RoleUpdateDtoValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Eklenecek rol adı boş olamaz.");
        }
    }
}

