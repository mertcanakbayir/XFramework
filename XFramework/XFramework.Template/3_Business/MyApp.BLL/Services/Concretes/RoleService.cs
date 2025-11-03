using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyApp.BLL.Services.Abstracts;
using MyApp.DAL.Entities;
using MyApp.Dtos.EndpointRole;
using MyApp.Dtos.PageRole;
using MyApp.Dtos.Role;
using MyApp.Helper.ViewModels;
using MyApp.Repository.Repositories.Abstract;

namespace MyApp.BLL.Services.Concretes
{
    public class RoleService : BaseService<Role, RoleDto, RoleAddDto, RoleUpdateDto>, IRegister
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly RoleAuthorizationService _roleAuthorizationService;
        private readonly IBaseRepository<PageRole> _pageRoleRepository;
        private readonly IBaseRepository<EndpointRole> _endpointRoleRepository;
        private readonly IValidator<PageRoleAddDto> _pageRoleAddDtoValidator;
        private readonly IValidator<EndpointRoleAddDto> _endpointRoleAddDtoValidator;
        public RoleService(IValidator<RoleAddDto> addDtoValidator, IMapper mapper, IBaseRepository<Role> baseRepository, IUnitOfWork unitOfWork, IValidator<RoleUpdateDto> updateDtoValidator, IBaseRepository<User> userRepository, RoleAuthorizationService roleAuthorizationService,
            IBaseRepository<EndpointRole> endpointRoleRepository, IBaseRepository<PageRole> pageRoleRepository, IValidator<PageRoleAddDto> pageRoleAddDtoValidator, IValidator<EndpointRoleAddDto> endpointRoleAddDtoValidator) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
            _userRepository = userRepository;
            _roleAuthorizationService = roleAuthorizationService;
            _pageRoleRepository = pageRoleRepository;
            _endpointRoleRepository = endpointRoleRepository;
            _pageRoleAddDtoValidator = pageRoleAddDtoValidator;
            _endpointRoleAddDtoValidator = endpointRoleAddDtoValidator;
        }

        public async Task<ResultViewModel<List<RoleDto>>> GetRolesByUser(int userId)
        {
            var user = await _userRepository.GetAsync(filter: u => u.Id == userId, include: i => i.Include(e => e.UserRoles).ThenInclude(r => r.Role));
            if (user == null || user.UserRoles == null || !user.UserRoles.Any())
            {
                return ResultViewModel<List<RoleDto>>.Failure("User has no roles", statusCode: 400);
            }
            var roles = user.UserRoles.Select(e => e.Role).ToList();
            var userRolesDto = _mapper.Map<List<RoleDto>>(roles);
            return ResultViewModel<List<RoleDto>>.Success(userRolesDto, "User roles:", 200);
        }
    }
}
