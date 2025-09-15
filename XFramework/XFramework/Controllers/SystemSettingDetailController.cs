using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos.SystemSettingDetail;
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
        [HttpGet]
        public async Task<ResultViewModel<List<SystemSettingDetailDto>>> GetSystemSettingDetailById(int systemSettingDetailId)
        {
            return await _systemSettingDetailService.GetAllAsync(e => e.Id == systemSettingDetailId);
        }

        [HttpPut("{id}")]
        public async Task<ResultViewModel<string>> UpdateSystemSettingDetail(SystemSettingDetailUpdateDto systemSettingDetailUpdateDto, int id)
        {
            return await _systemSettingDetailService.UpdateAsync(id, systemSettingDetailUpdateDto);
        }
    }
}
