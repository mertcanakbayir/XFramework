using Microsoft.AspNetCore.Mvc;
using MyApp.BLL.Services.Concretes;
using MyApp.Dtos.SystemSetting;
using MyApp.Helper.ViewModels;

namespace MyApp.API.Controllers
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

        [HttpPost]
        public async Task<ResultViewModel<string>> AddSystemSetting(SystemSettingAddDto systemSettingAddDto)
        {
            return await _systemSettingService.AddAsync(systemSettingAddDto);
        }
    }
}
