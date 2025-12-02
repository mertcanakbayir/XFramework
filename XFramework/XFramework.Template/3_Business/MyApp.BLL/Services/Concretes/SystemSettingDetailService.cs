using AutoMapper;
using FluentValidation;
using MyApp.BLL.Services.Abstracts;
using MyApp.DAL.Entities;
using MyApp.Dtos;
using MyApp.Dtos.SystemSettingDetail;
using MyApp.Helper.ViewModels;
using MyApp.Repository.Repositories.Abstract;

namespace MyApp.BLL.Services.Concretes
{
    public class SystemSettingDetailService : BaseService<SystemSettingDetail, SystemSettingDetailDto, SystemSettingDetailAddDto, SystemSettingDetailUpdateDto>, IRegister
    {

        private readonly SystemSettingService _systemSettingService;
        private readonly IValidator<SystemSettingDetailAddDto> _systemSettingsDetailAddDtoValidator;
        public SystemSettingDetailService(IValidator<SystemSettingDetailAddDto> addDtoValidator, IMapper mapper, IBaseRepository<SystemSettingDetail> baseRepository, IUnitOfWork unitOfWork, IValidator<SystemSettingDetailUpdateDto> updateDtoValidator, SystemSettingService systemSettingService) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
            _systemSettingService = systemSettingService;
            _systemSettingsDetailAddDtoValidator = addDtoValidator;
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

        public async Task<ResultViewModel<string>> AddAsync(int systemSettingId, SystemSettingDetailAddDto dto)
        {
            var validationResult = _systemSettingsDetailAddDtoValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return ResultViewModel<string>.Failure("Please check credentials", validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var systemSetting = await _systemSettingService.GetAsync(e => e.Id == systemSettingId);
            if (systemSetting == null)
            {
                return ResultViewModel<string>.Failure("System Setting Not Found", null, 404);
            }
            var entity = _mapper.Map<SystemSettingDetail>(dto);
            entity.SystemSettingId = systemSettingId;
            await _baseRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<string>.Success("System setting detail added succesfully.", 200);
        }
    }
}
