using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos.SystemSetting;
using XFramework.Helper.ViewModels;

namespace XFramework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemSettingController : ControllerBase
    {
        private readonly SystemSettingService _systemSettingService;
        public SystemSettingController(SystemSettingService systemSettingService)
        {
            _systemSettingService = systemSettingService;
        }
        [HttpGet("all")]
        public async Task<ResultViewModel<List<SystemSettingDto>>> SystemSettings()
        {
            return await _systemSettingService.GetAllAsync();
        }

        [HttpGet]
        public async Task<ResultViewModel<SystemSettingDto>> GetSystemSettingById(int systemSettingId)
        {
            return await _systemSettingService.GetAsync(e => e.Id == systemSettingId);
        }

        [HttpPut("{id}")]
        public async Task<ResultViewModel<string>> UpdateSystemSetting(int id, SystemSettingUpdateDto systemSettingUpdateDto)
        {
            return await _systemSettingService.UpdateAsync(id, systemSettingUpdateDto);
        }
    }
}
