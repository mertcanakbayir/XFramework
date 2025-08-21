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
        public RoleService(IBaseRepository<Role> roleRepository, IMapper mapper, IBaseRepository<Page> pageRepository, IBaseRepository<User> userRepository, IBaseRepository<PageRole> pageRoleRepository,
            IValidator<RoleAddDto> roleAddDtoValidator, IValidator<PageRoleAddDto> pageRoleAddDtoValidator, IBaseRepository<EndpointRole> endpointRoleRepository)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _pageRoleRepository = pageRoleRepository;
            _roleAddDtoValidator = roleAddDtoValidator;
            _pageRoleAddDtoValidator = pageRoleAddDtoValidator;
            _endpointRoleRepository = endpointRoleRepository;
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
            await _pageRoleRepository.AddAsync(pageRole);

            return ResultViewModel<string>.Success("Başarılı", "Kullanıcı Sayfa Yetkisi başarıyla eklendi.", 200);
        }

        public async Task<ResultViewModel<string>> AddEndpointRole(EndpointRoleAddDto endpointRoleAddDto)
        {

            var endpointRole = _mapper.Map<EndpointRole>(endpointRoleAddDto);
            _endpointRoleRepository.AddAsync(endpointRole);
            return ResultViewModel<string>.Success("Endpoint yetkisi başarıyla eklendi.", 200);
        }
    }
}
