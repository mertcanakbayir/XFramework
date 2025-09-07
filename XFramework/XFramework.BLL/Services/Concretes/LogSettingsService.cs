using Microsoft.EntityFrameworkCore;
using Serilog.Core;
using Serilog.Events;
using XFramework.DAL.Entities;
using XFramework.DAL.Migrations;
using XFramework.Repository.Repositories;

namespace XFramework.BLL.Services.Concretes
{
    public class LogSettingsService
    {
        private LoggingLevelSwitch _loggingLevelSwitch;
        private readonly IBaseRepository<SystemSettingDetail> _systemSettingDetailRepository;
        public LogSettingsService(IBaseRepository<SystemSettingDetail> systemSettingDetailRepository, LoggingLevelSwitch loggingLevelSwitch)
        {
            _systemSettingDetailRepository = systemSettingDetailRepository;
            _loggingLevelSwitch = loggingLevelSwitch;
        }
        public async Task<string> ToggleLogAsync(int systemSettingDetailId, bool isEnabled)
        {
            var logSetting = await _systemSettingDetailRepository.GetAsync(e => e.Id == systemSettingDetailId);
            if (logSetting == null)
            {
                return "hata";
            }

            logSetting.Value = isEnabled.ToString().ToLower();
            await _systemSettingDetailRepository.UpdateAsync(logSetting);
            _loggingLevelSwitch.MinimumLevel = isEnabled
               ? LogEventLevel.Warning
               : LogEventLevel.Fatal + 1;
            return "başarılı";
        }


    }
}
