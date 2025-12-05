using Microsoft.EntityFrameworkCore;
using XFramework.BLL.Services.Abstracts;
using XFramework.DAL.Entities;
using XFramework.Dtos;
using XFramework.Helper.ViewModels;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class UserRoleService : IRegister
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleService(IBaseRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<string>> AssignRolesAsync(UserRoleAssignDto userRoleAssignDto)
        {
            var user = await _userRepository.GetAsync(f => f.Id == userRoleAssignDto.UserId, include: q => q.Include(r => r.UserRoles), asNoTracking: false);

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
    }
}
