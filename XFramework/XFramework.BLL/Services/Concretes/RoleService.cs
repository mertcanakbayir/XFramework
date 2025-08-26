using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using XFramework.BLL.Result;
using XFramework.DAL.Abstract;
using XFramework.DAL.Entities;
using XFramework.Dtos;

namespace XFramework.BLL.Services.Concretes
{
    public class RoleService
    {
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<PageRole> _pageRoleRepository;
        private readonly IValidator<RoleAddDto> _roleAddDtoValidator;
        private readonly IValidator<PageRoleAddDto> _pageRoleAddDtoValidator;
        private readonly IBaseRepository<EndpointRole> _endpointRoleRepository;
        private readonly RoleAuthorizationService _roleAuthorizationService;
        private readonly CurrentUserService _currentUserService;
        public RoleService(IBaseRepository<Role> roleRepository, IMapper mapper, IBaseRepository<Page> pageRepository, IBaseRepository<User> userRepository, IBaseRepository<PageRole> pageRoleRepository,
            IValidator<RoleAddDto> roleAddDtoValidator, IValidator<PageRoleAddDto> pageRoleAddDtoValidator, IBaseRepository<EndpointRole> endpointRoleRepository, RoleAuthorizationService roleAuthorizationService,
            CurrentUserService currentUserService)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _pageRoleRepository = pageRoleRepository;
            _roleAddDtoValidator = roleAddDtoValidator;
            _pageRoleAddDtoValidator = pageRoleAddDtoValidator;
            _endpointRoleRepository = endpointRoleRepository;
            _roleAuthorizationService = roleAuthorizationService;
            _currentUserService = currentUserService;
        }

        public async Task<ResultViewModel<List<RoleDto>>> GetRolesByUser(int userId)
        {
            var user = await _userRepository.GetAsync(
         u => u.Id == userId,
         includeFunc: i => i.Include(e => e.UserRoles).ThenInclude(r => r.Role)
            );
            if (user == null || user.UserRoles == null || !user.UserRoles.Any())
            {
                return ResultViewModel<List<RoleDto>>.Failure("Kullanıcıya henüz rol atanmamış", statusCode: 400);
            }
            var roles = user.UserRoles.Select(e => e.Role).ToList();
            var userRolesDto = _mapper.Map<List<RoleDto>>(roles);
            return ResultViewModel<List<RoleDto>>.Success(userRolesDto, "Kullanıcı Rolleri:", 200);
        }

        public async Task<ResultViewModel<string>> AddRole(RoleAddDto roleAddDto)
        {
            var validationResult = _roleAddDtoValidator.Validate(roleAddDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultViewModel<string>.Failure("Rol eklenirken hata", errors, 400);
            }
            var role = _mapper.Map<Role>(roleAddDto);
            _roleRepository.GetCurrentUser(_currentUserService.GetUserId());
            await _roleRepository.AddAsync(role);
            return ResultViewModel<string>.Success("Role başarıyla eklendi", 200);
        }

        public async Task<ResultViewModel<string>> AddPageRole(PageRoleAddDto pageRoleAddDto)
        {
            var validationResult = _pageRoleAddDtoValidator.Validate(pageRoleAddDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultViewModel<string>.Failure("Rol eklenirken hata", errors, 400);
            }
            var pageRole = _mapper.Map<PageRole>(pageRoleAddDto);
            _pageRoleRepository.GetCurrentUser(_currentUserService.GetUserId());
            await _pageRoleRepository.AddAsync(pageRole);
            var usersWithRole = await _userRepository.GetAllAsync(u => u.UserRoles.Any(ur => ur.RoleId == pageRoleAddDto.RoleId));
            foreach (var user in usersWithRole)
            {
                _roleAuthorizationService.ClearUserPageCache(user.Id);
            }
            return ResultViewModel<string>.Success("Başarılı", "Kullanıcı Sayfa Yetkisi başarıyla eklendi.", 200);
        }

        public async Task<ResultViewModel<string>> AddEndpointRole(EndpointRoleAddDto endpointRoleAddDto)
        {
            var endpointRole = _mapper.Map<EndpointRole>(endpointRoleAddDto);
            _endpointRoleRepository.GetCurrentUser(_currentUserService.GetUserId());
            await _endpointRoleRepository.AddAsync(endpointRole);
            var usersWithRole = await _userRepository.GetAllAsync(u => u.UserRoles.Any(ur => ur.RoleId == endpointRoleAddDto.RoleId));
            _endpointRoleRepository.GetCurrentUser(_currentUserService.GetUserId());
            foreach (var user in usersWithRole)
            {
                _roleAuthorizationService.ClearUserEndpointCache(user.Id);
            }
            return ResultViewModel<string>.Success("Endpoint yetkisi başarıyla eklendi.", 200);
        }
    }
}
