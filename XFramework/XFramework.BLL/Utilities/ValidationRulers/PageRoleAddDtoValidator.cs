using FluentValidation;
using XFramework.Dtos.PageRole;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class PageRoleAddDtoValidator : AbstractValidator<PageRoleAddDto>
    {
        public PageRoleAddDtoValidator()
        {
            RuleFor(x => x.PageId).NotEmpty().WithMessage("Sayfa ID girilimelidir.");

            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Rol ID girilmelidir.");
        }
    }
}
