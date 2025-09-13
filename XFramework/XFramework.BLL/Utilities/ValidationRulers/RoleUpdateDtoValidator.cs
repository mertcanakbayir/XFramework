using FluentValidation;
using XFramework.Dtos.Role;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class RoleUpdateDtoValidator : AbstractValidator<RoleUpdateDto>
    {
        public RoleUpdateDtoValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Eklenecek rol adı boş olamaz.");
        }
    }
}

