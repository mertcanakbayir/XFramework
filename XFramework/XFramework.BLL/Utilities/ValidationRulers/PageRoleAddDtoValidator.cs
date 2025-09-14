using FluentValidation;
using XFramework.Dtos.PageRole;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class PageRoleAddDtoValidator : AbstractValidator<PageRoleAddDto>
    {
        public PageRoleAddDtoValidator()
        {
            RuleFor(x => x.PageId).NotEmpty().WithMessage("Page ID is required.");

            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role ID is required.");
        }
    }
}
