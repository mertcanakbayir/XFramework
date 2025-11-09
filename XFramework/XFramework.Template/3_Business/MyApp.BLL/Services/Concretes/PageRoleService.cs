using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyApp.BLL.Services.Abstracts;
using MyApp.DAL.Entities;
using MyApp.Dtos.PageRole;
using MyApp.Dtos.User;
using MyApp.Helper.ViewModels;
using MyApp.Repository.Repositories.Abstract;

namespace MyApp.BLL.Services.Concretes
{
    public class PageRoleService : BaseService<PageRole, PageRoleDto, PageRoleAddDto, PageRoleUpdateDto>, IRegister
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly RoleAuthorizationService _roleAuthorizationService;
        public PageRoleService(IValidator<PageRoleAddDto> addDtoValidator, IMapper mapper, IBaseRepository<PageRole> baseRepository, IUnitOfWork unitOfWork, IValidator<PageRoleUpdateDto> updateDtoValidator,
            IBaseRepository<User> userRepository, RoleAuthorizationService roleAuthorizationService) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
            _userRepository = userRepository;
            _roleAuthorizationService = roleAuthorizationService;
        }
        public async Task<ResultViewModel<string>> AddPageRole(PageRoleAddDto pageRoleAddDto)
        {
            var validationResult = _addDtoValidator.Validate(pageRoleAddDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultViewModel<string>.Failure("Error adding role", errors, 400);
            }
            var pageRole = _mapper.Map<PageRole>(pageRoleAddDto);
            await _baseRepository.AddAsync(pageRole);
            await _unitOfWork.SaveChangesAsync();
            var usersWithPageRole = await _userRepository.GetAllAsync<UserDto>(filter: q => q.UserRoles.Any(r => r.RoleId == pageRoleAddDto.RoleId),
                include: q => q.Include(u => u.UserRoles));
            foreach (var user in usersWithPageRole.Data)
            {
                _roleAuthorizationService.ClearUserPageCache(user.Id);
            }
            return ResultViewModel<string>.Success("User page permission added succesfully.", 200);
        }
    }
}
