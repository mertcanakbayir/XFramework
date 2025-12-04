using AutoMapper;
using XFramework.Dtos;
using XFramework.Helper.Enums;

namespace XFramework.BLL.Mappings
{
    public class MailSettingsProfile : Profile
    {
        public MailSettingsProfile()
        {
            CreateMap<Dictionary<string, string>, MailSettingDto>()
                .ForMember(dest => dest.SmtpHost,
                    opt => opt.MapFrom(src => src.GetValueOrDefault(MailSettingKeys.SmtpHost.ToString()) ?? string.Empty))
                .ForMember(dest => dest.SmtpPort,
                    opt => opt.MapFrom(src => int.Parse(src.GetValueOrDefault(MailSettingKeys.SmtpPort.ToString()) ?? "25")))
                .ForMember(dest => dest.SmtpUser,
                    opt => opt.MapFrom(src => src.GetValueOrDefault(MailSettingKeys.SmtpUser.ToString()) ?? string.Empty))
                .ForMember(dest => dest.SmtpPassword,
                    opt => opt.MapFrom(src => src.GetValueOrDefault(MailSettingKeys.SmtpPassword.ToString()) ?? string.Empty))
                .ForMember(dest => dest.SenderEmail,
                    opt => opt.MapFrom(src => src.GetValueOrDefault(MailSettingKeys.SenderEmail.ToString()) ?? string.Empty));
        }
    }
}
