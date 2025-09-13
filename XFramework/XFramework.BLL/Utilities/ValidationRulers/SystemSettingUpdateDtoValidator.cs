using FluentValidation;
using XFramework.Dtos.SystemSetting;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class SystemSettingUpdateDtoValidator : AbstractValidator<SystemSettingUpdateDto>
    {
        public SystemSettingUpdateDtoValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Sistem ayarı ismi girmek zorunludur.");
            RuleFor(e => e.Description).NotEmpty().WithMessage("Sistem ayarı açıklaması zorunludur");
        }
    }
}
