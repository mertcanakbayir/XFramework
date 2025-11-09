using FluentValidation;
using MyApp.Dtos.PageRole;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class PageRoleUpdateDtoValidator : AbstractValidator<PageRoleUpdateDto>
    {
        public PageRoleUpdateDtoValidator()
        {
            RuleFor(x => x.PageId).NotEmpty().WithMessage("Page ID is required.");

            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role ID is required.");
        }
    }
}
