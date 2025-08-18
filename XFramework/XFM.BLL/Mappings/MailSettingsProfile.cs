using AutoMapper;
using XFramework.DAL.Enums;
using XFramework.Dtos;

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
                .ForMember(dest => dest.EncryptedPassword,
                    opt => opt.MapFrom(src => src.GetValueOrDefault(MailSettingKeys.EncryptedPassword.ToString()) ?? string.Empty))
                .ForMember(dest => dest.EnableSsl,
                    opt => opt.MapFrom(src => bool.Parse(src.GetValueOrDefault(MailSettingKeys.EnableSsl.ToString()) ?? "false")))
                .ForMember(dest => dest.SenderEmail,
                    opt => opt.MapFrom(src => src.GetValueOrDefault(MailSettingKeys.SenderEmail.ToString()) ?? string.Empty));
        }
    }
}
