using FluentValidation;
using XFramework.Dtos.SystemSetting;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class SystemSettingAddDtoValidator : AbstractValidator<SystemSettingAddDto>
    {
        public SystemSettingAddDtoValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Sistem ayarı ismi girmek zorunludur.");
            RuleFor(e => e.Description).NotEmpty().WithMessage("Sistem ayarı açıklaması zorunludur");
        }
    }
}
