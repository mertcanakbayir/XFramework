using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XFramework.BLL.Services.Abstracts;
using XFramework.DAL.Entities;
using XFramework.Dtos;
using XFramework.Dtos.Role;
using XFramework.Helper.ViewModels;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class UserRoleService : IRegister
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Role> _roleRepository;

        public UserRoleService(IBaseRepository<User> userRepository, IBaseRepository<Role> roleRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<ResultViewModel<string>> AssignRolesAsync(UserRoleAssignDto userRoleAssignDto)
        {
            var roles = await _roleRepository.GetAllAsync<RoleDto>();
            var validRoleIds = roles.Data.Select(r => r.Id).ToList();
            var invalidRoleIds = userRoleAssignDto.RoleIds.Except(validRoleIds);

            if (invalidRoleIds.Any())
            {
                return ResultViewModel<string>.Failure(
                "Invalid role(s) provided",
                errors: invalidRoleIds.Select(x => $"RoleId not found").ToList(),
                statusCode: 400
            );
            }

            var user = await _userRepository.GetAsync(
                f => f.Id == userRoleAssignDto.UserId,
                include: q => q.Include(r => r.UserRoles),
                asNoTracking: false);

            if (user == null)
                return ResultViewModel<string>.Failure("User not found", null, 404);

            var currentRoleIds = user.UserRoles.Select(e => e.RoleId).ToList();
            var newRoleIds = userRoleAssignDto.RoleIds;

            var rolesToRemove = currentRoleIds.Except(newRoleIds).ToList();
            var rolesToAdd = newRoleIds.Except(currentRoleIds).ToList();

            foreach (var roleId in rolesToRemove)
            {
                var userRoleEntity = user.UserRoles.FirstOrDefault(ur => ur.RoleId == roleId);
                if (userRoleEntity != null)
                {
                    user.UserRoles.Remove(userRoleEntity);
                }
            }

            foreach (var roleId in rolesToAdd)
            {
                user.UserRoles.Add(new UserRole
                {
                    UserId = user.Id,
                    RoleId = roleId,
                });
            }
            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<string>.Success("Roles updated successfully.", 200);
        }
        public async Task<ResultViewModel<UserRoleDto>> GetAssignedRolesAsync(int userId)
        {
            var user = await _userRepository.GetAsync(
                e => e.Id == userId,
                include: q => q.Include(u => u.UserRoles).ThenInclude(r => r.Role));

            if (user == null)
            {
                return ResultViewModel<UserRoleDto>.Failure("User not found", null, 404);
            }

            var dto = _mapper.Map<UserRoleDto>(user);

            return ResultViewModel<UserRoleDto>.Success(dto, "User roles retrieved successfully.", 200);
        }
    }
}
