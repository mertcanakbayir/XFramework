using AutoMapper;
using FluentValidation;
using XFramework.DAL.Entities;
using XFramework.Dtos;
using XFramework.Helper.ViewModels;
using XFramework.Repository.Repositories;

namespace XFramework.BLL.Services.Concretes
{
    public class SystemSettingService
    {
        private readonly BaseRepository<SystemSetting> _systemSettingRepository;
        private readonly IMapper _mapper;
        private readonly CurrentUserService _currentUserService;
        private readonly IValidator<SystemSettingDto> _systemSettingDtoValidator;

        public SystemSettingService(BaseRepository<SystemSetting> systemSettingRepository, IMapper mapper, CurrentUserService currentUserService,
            IValidator<SystemSettingDto> systemSettingDtoValidator)
        {
            _systemSettingRepository = systemSettingRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _systemSettingDtoValidator = systemSettingDtoValidator;
        }

        public async Task<ResultViewModel<List<SystemSettingDto>>> GetSystemSettings()
        {
            var systemSettings = await _systemSettingRepository.GetAllAsync();
            if (systemSettings == null)
            {
                return ResultViewModel<List<SystemSettingDto>>.Failure("Sistem Ayarı bulunamadı.", null, 400);
            }
            var systemSettingsDto = _mapper.Map<List<SystemSettingDto>>(systemSettings);
            return ResultViewModel<List<SystemSettingDto>>.Success(systemSettingsDto, "Sistem Ayarları:", 200);
        }

        public async Task<ResultViewModel<SystemSettingDto>> GetSystemSettingById(int systemSettingId)
        {
            var systemSetting = await _systemSettingRepository.GetAsync(e => e.Id == systemSettingId);
            if (systemSetting == null)
            {
                return ResultViewModel<SystemSettingDto>.Failure("Sistem Ayarı Bulunamadı.", null, 200);
            }
            var systemSettingDto = _mapper.Map<SystemSettingDto>(systemSetting);
            return ResultViewModel<SystemSettingDto>.Success(systemSettingDto, "Sistem Ayarı:", 200);
        }

        public async Task<ResultViewModel<SystemSettingDto>> UpdateSystemSetting(SystemSettingDto systemSettingDto, int id)
        {
            var validationResult = _systemSettingDtoValidator.Validate(systemSettingDto);
            if (!validationResult.IsValid)
            {
                return ResultViewModel<SystemSettingDto>.Failure("Girdiğiniz bilgileri kontrol edin.", validationResult.Errors.Select(e => e.ErrorMessage).ToList(), 400);
            }
            var systemSettingEntity = await _systemSettingRepository.GetAsync(e => e.Id == id);
            if (systemSettingEntity == null)
            {
                return ResultViewModel<SystemSettingDto>.Failure("Sistem ayarı bulunamadı.", null, 404);
            }

            _mapper.Map(systemSettingDto, systemSettingEntity);
            _systemSettingRepository.GetCurrentUser(_currentUserService.GetUserId());
            await _systemSettingRepository.UpdateAsync(systemSettingEntity);
            return ResultViewModel<SystemSettingDto>.Success("Sistem Ayarı Güncellendi");
        }
    }
}
