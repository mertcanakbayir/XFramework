using FluentValidation;
using MyApp.Dtos.SystemSettingDetail;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class SystemSettingDetailUpdateDtoValidator : AbstractValidator<SystemSettingDetailUpdateDto>
    {
        public SystemSettingDetailUpdateDtoValidator()
        {
            RuleFor(e => e.Key)
                .NotEmpty().WithMessage("System detail key cannot be empty.");

            RuleFor(e => e.Value)
                .NotEmpty().WithMessage("System detail value cannot be empty.");

            RuleFor(e => e.Type)
                .NotEmpty().WithMessage("System detail type cannot be empty.");

        }
    }
}
