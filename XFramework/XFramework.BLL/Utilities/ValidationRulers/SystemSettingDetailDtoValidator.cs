using FluentValidation;
using XFramework.Dtos;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class SystemSettingDetailDtoValidator : AbstractValidator<SystemSettingDetailDto>
    {
        public SystemSettingDetailDtoValidator()
        {
            RuleFor(e => e.Key).NotEmpty().WithMessage("Sistem Detay Anahtarı Boş Olamaz");
            RuleFor(e => e.Value).NotEmpty().WithMessage("Sistem Detay Değeri Boş Olamaz");
            RuleFor(e => e.Type).NotEmpty().WithMessage("Sistem Detay Tip Değeri Boş Olamaz");
        }
    }
}
