using Serilog.Core;
using Serilog.Events;
using XFramework.BLL.Services.Abstracts;
using XFramework.DAL.Entities;
using XFramework.Repository.Options;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class LogSettingsService : IRegister
    {
        private LoggingLevelSwitch _loggingLevelSwitch;
        private readonly IBaseRepository<SystemSettingDetail> _systemSettingDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        public LogSettingsService(IBaseRepository<SystemSettingDetail> systemSettingDetailRepository, LoggingLevelSwitch loggingLevelSwitch, IUnitOfWork unitOfWork)
        {
            _systemSettingDetailRepository = systemSettingDetailRepository;
            _loggingLevelSwitch = loggingLevelSwitch;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> ToggleLogAsync(int systemSettingDetailId, bool isEnabled)
        {
            var logSetting = await _systemSettingDetailRepository.GetAsync(new BaseRepoOptions<SystemSettingDetail>
            {
                Filter = e => e.Id == systemSettingDetailId,
            });
            if (logSetting == null)
            {
                return "Log Setting Switch Error";
            }

            logSetting.Value = isEnabled.ToString().ToLower();
            await _systemSettingDetailRepository.UpdateAsync(logSetting);
            await _unitOfWork.SaveChangesAsync();
            _loggingLevelSwitch.MinimumLevel = isEnabled
               ? LogEventLevel.Warning
               : LogEventLevel.Fatal + 1;
            return "Log Setting Switch Success";
        }
    }
}
