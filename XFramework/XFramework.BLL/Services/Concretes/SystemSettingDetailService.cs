using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos;
using XFramework.Helper.ViewModels;
using XFramework.Repository.Options;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class SystemSettingDetailService
    {
        private readonly IBaseRepository<SystemSettingDetail> _systemSettingDetailRepository;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SystemSettingDetailService(IBaseRepository<SystemSettingDetail> systemSettingDetailRepository, IMapper mapper, IBaseRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _systemSettingDetailRepository = systemSettingDetailRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<List<SystemSettingDetailDto>>> GetSystemSettingDetails(int systemSettingId)
        {
            var systemSettingDetailEntity = await _systemSettingDetailRepository.GetAllAsync(new BaseRepoOptions<SystemSettingDetail>
            {

            });
            if (systemSettingDetailEntity == null)
            {
                return ResultViewModel<List<SystemSettingDetailDto>>.Failure("Sistem Detayı bulunamadı.", null, 404);
            }
            var systemSettingDetailDto = _mapper.Map<List<SystemSettingDetailDto>>(systemSettingDetailEntity);
            return ResultViewModel<List<SystemSettingDetailDto>>.Success(systemSettingDetailDto, "Sistem Ayarı Ayrıntıları:", 200);
        }

        public async Task<ResultViewModel<SystemSettingDetailDto>> GetSystemSettingDetailByDetailId(int systemSettingId, int systemSettingDetailId)
        {
            var systemSettingDetailEntity = await _systemSettingDetailRepository.GetAsync(new BaseRepoOptions<SystemSettingDetail>
            {
                Filter = e => e.SystemSettingId == systemSettingId && e.Id == systemSettingDetailId,
            });
            if (systemSettingDetailEntity == null)
            {
                return ResultViewModel<SystemSettingDetailDto>.Failure("Sistem Detayı Bulunamadı.", null, 404);
            }
            var systemSettingDetailDto = _mapper.Map<SystemSettingDetailDto>(systemSettingDetailEntity);
            return ResultViewModel<SystemSettingDetailDto>.Success(systemSettingDetailDto, "Sistem Ayarı:", 200);
        }

        public async Task<ResultViewModel<string>> UpdateSystemSettingDetail(SystemSettingDetailDto systemSettingDetailDto, int id)
        {
            var systemSettingDetailEntity = await _systemSettingDetailRepository.GetAsync(new BaseRepoOptions<SystemSettingDetail>
            {
                Filter = e => e.SystemSettingId == systemSettingDetailDto.SystemSettingId && e.Id == id
            });
            if (systemSettingDetailEntity == null)
            {
                return ResultViewModel<string>.Failure("Sistem Ayar Detayı Bulunamadı", null, 404);
            }
            _mapper.Map(systemSettingDetailDto, systemSettingDetailEntity);
            await _systemSettingDetailRepository.UpdateAsync(systemSettingDetailEntity);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<string>.Success("Sistem Ayar Detayı güncellendi", 200);
        }
    }
}
