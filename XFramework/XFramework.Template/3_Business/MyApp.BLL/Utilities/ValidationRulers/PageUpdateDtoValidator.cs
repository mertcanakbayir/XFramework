using FluentValidation;
using MyApp.Dtos.Page;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class PageUpdateDtoValidator : AbstractValidator<PageUpdateDto>
    {
        public PageUpdateDtoValidator()
        {
            RuleFor(x => x.PageUrl).NotEmpty().WithMessage("Page Url is required.").Must(url => url.StartsWith("/")).WithMessage("Page Url must start with / .");
        }
    }
}
