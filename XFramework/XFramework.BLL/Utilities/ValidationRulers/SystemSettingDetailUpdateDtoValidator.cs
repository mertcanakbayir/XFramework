using FluentValidation;
using XFramework.Dtos.SystemSettingDetail;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class SystemSettingDetailUpdateDtoValidator : AbstractValidator<SystemSettingDetailUpdateDto>
    {
        public SystemSettingDetailUpdateDtoValidator()
        {
            RuleFor(e => e.Key).NotEmpty().WithMessage("Sistem Detay Anahtarı Boş Olamaz");
            RuleFor(e => e.Value).NotEmpty().WithMessage("Sistem Detay Değeri Boş Olamaz");
            RuleFor(e => e.Type).NotEmpty().WithMessage("Sistem Detay Tip Değeri Boş Olamaz");
        }
    }
}
