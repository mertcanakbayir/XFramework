using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos;
using XFramework.Helper.ViewModels;

namespace XFramework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemSettingDetailController : ControllerBase
    {
        private readonly SystemSettingDetailService _systemSettingDetailService;

        public SystemSettingDetailController(SystemSettingDetailService systemSettingDetailService)
        {
            _systemSettingDetailService = systemSettingDetailService;
        }

        [HttpGet("all")]
        public async Task<ResultViewModel<List<SystemSettingDetailDto>>> GetSystemSettingDetailById(int systemSettingDetailId)
        {
            return await _systemSettingDetailService.GetSystemSettingDetails(systemSettingDetailId);
        }

        [HttpGet]
        public async Task<ResultViewModel<SystemSettingDetailDto>> GetSystemSettingDetailByDetailId(int systemSettingDetailId, int systemSettingId)
        {
            return await _systemSettingDetailService.GetSystemSettingDetailByDetailId(systemSettingId, systemSettingDetailId);
        }

        [HttpPut("{id}")]
        public async Task<ResultViewModel<string>> UpdateSystemSettingDetail(SystemSettingDetailDto systemSettingDetailDto, int id)
        {
            return await _systemSettingDetailService.UpdateSystemSettingDetail(systemSettingDetailDto, id);
        }
    }
}
