using FluentValidation;
using XFramework.Dtos.Page;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class PageAddDtoValidator : AbstractValidator<PageAddDto>
    {
        public PageAddDtoValidator()
        {
            RuleFor(x => x.PageUrl).NotEmpty().WithMessage("Page address is required.").Must(url => url.StartsWith('/')).WithMessage("Page address must start with '/'.");
        }
    }
}
