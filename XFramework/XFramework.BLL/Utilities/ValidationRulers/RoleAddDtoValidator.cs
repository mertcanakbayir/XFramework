using FluentValidation;
using XFramework.Dtos.Role;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class RoleAddDtoValidator : AbstractValidator<RoleAddDto>
    {
        public RoleAddDtoValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Role name to be added cannot be empty.");

        }
    }
}
