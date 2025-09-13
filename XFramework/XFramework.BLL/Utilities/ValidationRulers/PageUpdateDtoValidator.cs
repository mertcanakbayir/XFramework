using FluentValidation;
using XFramework.Dtos.Page;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class PageUpdateDtoValidator : AbstractValidator<PageUpdateDto>
    {
        public PageUpdateDtoValidator()
        {
            RuleFor(x => x.PageUrl).NotEmpty().WithMessage("Page Url can not be null").Must(url => url.StartsWith("/")).WithMessage("Sayfa Url / ile başlamalı");
        }
    }
}
