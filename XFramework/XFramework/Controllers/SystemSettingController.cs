using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Result;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos;

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
            return await _systemSettingService.GetSystemSettings();
        }

        [HttpGet]
        public async Task<ResultViewModel<SystemSettingDto>> GetSystemSettingById(int systemSettingId)
        {
            return await _systemSettingService.GetSystemSettingById(systemSettingId);
        }

        [HttpPut("{id}")]
        public async Task<ResultViewModel<SystemSettingDto>> UpdateSystemSetting(int id, SystemSettingDto systemSettingDto)
        {
            return await _systemSettingService.UpdateSystemSetting(systemSettingDto, id);
        }
    }
}
