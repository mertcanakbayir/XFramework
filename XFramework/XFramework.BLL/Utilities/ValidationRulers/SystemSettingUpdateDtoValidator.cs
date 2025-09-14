using FluentValidation;
using XFramework.Dtos.SystemSetting;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class SystemSettingUpdateDtoValidator : AbstractValidator<SystemSettingUpdateDto>
    {
        public SystemSettingUpdateDtoValidator()
        {
            RuleFor(e => e.Name)
    .NotEmpty().WithMessage("System setting name is required.");

            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("System setting description is required.");

        }
    }
}
