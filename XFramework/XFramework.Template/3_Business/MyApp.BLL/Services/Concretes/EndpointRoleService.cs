using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyApp.BLL.Services.Abstracts;
using MyApp.DAL.Entities;
using MyApp.Dtos.EndpointRole;
using MyApp.Dtos.User;
using MyApp.Helper.ViewModels;
using MyApp.Repository.Repositories.Abstract;

namespace MyApp.BLL.Services.Concretes
{
    public class EndpointRoleService : BaseService<EndpointRole, EndpointRoleDto, EndpointRoleAddDto, EndpointRoleUpdateDto>, IRegister
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly RoleAuthorizationService _roleAuthorizationService;
        public EndpointRoleService(IValidator<EndpointRoleAddDto> addDtoValidator, IMapper mapper, IBaseRepository<EndpointRole> baseRepository, IUnitOfWork unitOfWork, IValidator<EndpointRoleUpdateDto> updateDtoValidator,
            IBaseRepository<User> userRepository, RoleAuthorizationService roleAuthorizationService) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
            _userRepository = userRepository;
            _roleAuthorizationService = roleAuthorizationService;
        }

        public async Task<ResultViewModel<string>> AddEndpointRole(EndpointRoleAddDto endpointRoleAddDto)
        {
            var validationResult = _addDtoValidator.Validate(endpointRoleAddDto);
            if (!validationResult.IsValid)
            {
                return ResultViewModel<string>.Failure("Check credentials", errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var endpoint = _mapper.Map<EndpointRole>(endpointRoleAddDto);
            await _baseRepository.AddAsync(endpoint);
            await _unitOfWork.SaveChangesAsync();

            var usersWithEndpointRole = await _userRepository.GetAllAsync<UserDto>(filter: u => u.UserRoles.Any(ur => ur.RoleId == endpointRoleAddDto.RoleId), include: i => i.Include(u => u.UserRoles));

            foreach (var user in usersWithEndpointRole.Data)
            {
                _roleAuthorizationService.ClearUserEndpointCache(user.Id);
            }
            return ResultViewModel<string>.Success(message: "User endpoint permission added successfully");
        }
    }
}
