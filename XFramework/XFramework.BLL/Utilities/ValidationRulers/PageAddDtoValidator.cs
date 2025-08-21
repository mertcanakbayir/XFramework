using FluentValidation;
using XFramework.Dtos;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class PageAddDtoValidator : AbstractValidator<PageAddDto>
    {
        public PageAddDtoValidator()
        {
            RuleFor(x => x.PageUrl).NotEmpty().WithMessage("Sayfa Adresi boş olamaz").Must(url => url.StartsWith('/')).WithMessage("Sayfa adresi '/' ile başlamalıdır.");
        }
    }
}
