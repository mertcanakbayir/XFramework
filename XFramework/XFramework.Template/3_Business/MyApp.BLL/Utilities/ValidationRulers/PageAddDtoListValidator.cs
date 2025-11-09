using FluentValidation;
using MyApp.Dtos.Page;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class PageAddDtoListValidator : AbstractValidator<List<PageAddDto>>
    {
        public PageAddDtoListValidator(IValidator<PageAddDto> itemValidator)
        {
            RuleForEach(x => x).SetValidator(itemValidator);
        }
    }
}
