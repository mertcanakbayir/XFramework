using FluentValidation;
using XFramework.Dtos.Page;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class PageAddDtoListValidator : AbstractValidator<List<PageAddDto>>
    {
        public PageAddDtoListValidator(IValidator<PageAddDto> itemValidator)
        {
            RuleForEach(x => x).SetValidator(itemValidator);
        }
    }
}
