using FluentValidation;
using XFramework.Dtos;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class SystemSettingDtoValidator : AbstractValidator<SystemSettingDto>
    {
        public SystemSettingDtoValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Sistem ayarı ismi girmek zorunludur.");
            RuleFor(e => e.Description).NotEmpty().WithMessage("Sistem ayarı açıklaması zorunludur");
        }
    }
}
