using AutoMapper;
using FluentValidation;
using XFramework.BLL.Services.Abstracts;
using XFramework.Configuration;
using XFramework.DAL.Entities;
using XFramework.Dtos.SystemSettingDetail;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class SystemSettingDetailService : BaseService<SystemSettingDetail, SystemSettingDetailDto, SystemSettingDetailAddDto, SystemSettingDetailUpdateDto>, IRegister
    {
        public SystemSettingDetailService(IValidator<SystemSettingDetailAddDto> addDtoValidator, IMapper mapper, IBaseRepository<SystemSettingDetail> baseRepository, IUnitOfWork unitOfWork, IValidator<SystemSettingDetailUpdateDto> updateDtoValidator) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
        }

        public async Task<MailOptions> GetMailOptionsAsync(int systemSettingsId)
        {
            var details = await _baseRepository.GetAllAsync<SystemSettingDetailDto>(filter: e => e.SystemSettingId == systemSettingsId);

            var dict = details.Data.ToDictionary(d => d.Key, d => d.Value);

            return new MailOptions
            {
                SmtpHost = dict.GetValueOrDefault("SmtpHost", ""),
                SmtpPort = int.TryParse(dict.GetValueOrDefault("SmtpPort"), out var port) ? port : 25,
                SmtpUser = dict.GetValueOrDefault("SmtpUser", ""),
                EncryptedPassword = dict.GetValueOrDefault("EncryptedPassword", ""),
                EnableSsl = bool.TryParse(dict.GetValueOrDefault("EnableSsl"), out var ssl) && ssl,
                SenderEmail = dict.GetValueOrDefault("SenderEmail", ""),
                IsQueue = bool.TryParse(dict.GetValueOrDefault("IsQueue"), out var q) && q
            };
        }
    }
}
