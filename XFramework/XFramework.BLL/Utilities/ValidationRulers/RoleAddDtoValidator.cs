using FluentValidation;
using XFramework.Dtos;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class RoleAddDtoValidator : AbstractValidator<RoleAddDto>
    {
        public RoleAddDtoValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Eklenecek rol adı boş olamaz.");
        }
    }
}
