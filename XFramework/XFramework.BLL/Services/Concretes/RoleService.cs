using AutoMapper;
using Dtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using XFramework.DAL.Entities;
using XFramework.Dtos;
using XFramework.Dtos.EndpointRole;
using XFramework.Dtos.PageRole;
using XFramework.Dtos.Role;
using XFramework.Helper.ViewModels;
using XFramework.Repository.Options;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class RoleService : BaseService<Role, RoleDto, RoleAddDto, RoleUpdateDto>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly RoleAuthorizationService _roleAuthorizationService;
        private readonly IBaseRepository<PageRole> _pageRoleRepository;
        private readonly IBaseRepository<EndpointRole> _endpointRoleRepository;
        public RoleService(IValidator<RoleAddDto> addDtoValidator, IMapper mapper, IBaseRepository<Role> baseRepository, IUnitOfWork unitOfWork, IValidator<RoleUpdateDto> updateDtoValidator, IBaseRepository<User> userRepository, RoleAuthorizationService roleAuthorizationService,
            IBaseRepository<EndpointRole> endpointRoleRepository, IBaseRepository<PageRole> pageRoleRepository) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
            _userRepository = userRepository;
            _roleAuthorizationService = roleAuthorizationService;
            _pageRoleRepository = pageRoleRepository;
            _endpointRoleRepository = endpointRoleRepository;
        }

        public async Task<ResultViewModel<List<RoleDto>>> GetRolesByUser(int userId)
        {
            var user = await _userRepository.GetAsync(new BaseRepoOptions<User>
            {
                Filter = u => u.Id == userId,
                IncludeFunc = i => i.Include(e => e.UserRoles).ThenInclude(r => r.Role)
            });
            if (user == null || user.UserRoles == null || !user.UserRoles.Any())
            {
                return ResultViewModel<List<RoleDto>>.Failure("Kullanıcıya henüz rol atanmamış", statusCode: 400);
            }
            var roles = user.UserRoles.Select(e => e.Role).ToList();
            var userRolesDto = _mapper.Map<List<RoleDto>>(roles);
            return ResultViewModel<List<RoleDto>>.Success(userRolesDto, "Kullanıcı Rolleri:", 200);
        }
        public async Task<ResultViewModel<string>> AddPageRole(PageRoleAddDto pageRoleAddDto)
        {
            //var validationResult = _pageRoleAddDtoValidator.Validate(pageRoleAddDto);
            //if (!validationResult.IsValid)
            //{
            //    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            //    return ResultViewModel<string>.Failure("Rol eklenirken hata", errors, 400);
            //}
            var pageRole = _mapper.Map<PageRole>(pageRoleAddDto);
            await _pageRoleRepository.AddAsync(pageRole);
            await _unitOfWork.SaveChangesAsync();
            var usersWithPageRole = await _userRepository.GetAllAsync<UserDto>(new BaseRepoOptions<User>
            {
                Filter = q => q.UserRoles.Any(r => r.RoleId == pageRoleAddDto.RoleId),
                IncludeFunc = query => query.Include(u => u.UserRoles)
            });
            foreach (var user in usersWithPageRole)
            {
                _roleAuthorizationService.ClearUserPageCache(user.Id);
            }
            return ResultViewModel<string>.Success("Başarılı", "Kullanıcı Sayfa Yetkisi başarıyla eklendi.", 200);
        }
        public async Task<ResultViewModel<string>> AddEndpointRole(EndpointRoleAddDto endpointRoleAddDto)
        {
            var endpointRole = _mapper.Map<EndpointRole>(endpointRoleAddDto);
            await _endpointRoleRepository.AddAsync(endpointRole);
            await _unitOfWork.SaveChangesAsync();
            var usersWithEndpointRole = await _userRepository.GetAllAsync<UserDto>(
                new BaseRepoOptions<User>
                {
                    Filter = u => u.UserRoles.Any(ur => ur.RoleId == endpointRoleAddDto.RoleId),
                    IncludeFunc = query => query.Include(u => u.UserRoles)
                });
            foreach (var user in usersWithEndpointRole)
            {
                _roleAuthorizationService.ClearUserEndpointCache(user.Id);
            }
            return ResultViewModel<string>.Success("Endpoint yetkisi başarıyla eklendi.", 200);
        }
    }
}
