using FluentValidation;
using MyApp.Dtos.PageRole;

namespace MyApp.BLL.Utilities.ValidationRulers
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
