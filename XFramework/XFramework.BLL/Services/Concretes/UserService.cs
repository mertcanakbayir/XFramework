using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using XFramework.BLL.Services.Abstracts;
using XFramework.BLL.Utilities.Hashing;
using XFramework.DAL.Entities;
using XFramework.Dtos.User;
using XFramework.Helper.ViewModels;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class UserService : BaseService<User, UserDto, UserAddDto, UserUpdateDto>, IRegister
    {

        private readonly IHashingHelper _hashingHelper;

        public UserService(IValidator<UserAddDto> addDtoValidator, IMapper mapper, IBaseRepository<User> baseRepository, IUnitOfWork unitOfWork, IValidator<UserUpdateDto> updateDtoValidator, IHashingHelper hashingHelper) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
            _hashingHelper = hashingHelper;
        }

        public async Task<ResultViewModel<UserAddDto>> AddUser(UserAddDto userAddDto)
        {
            var validationResult = _addDtoValidator.Validate(userAddDto);
            if (!validationResult.IsValid)
            {
                return ResultViewModel<UserAddDto>.Failure("Please check credentials.", validationResult.Errors.Select(e => e.ErrorMessage).ToList(), 400);
            }
            var userEntity = _mapper.Map<User>(userAddDto);
            userEntity.Password = _hashingHelper.HashPassword(userEntity.Password);
            await _baseRepository.AddAsync(userEntity);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<UserAddDto>.Success("User added succesfully", 201);
        }

        public async Task<ResultViewModel<UserUpdateDto>> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            var validationResult = _updateDtoValidator.Validate(userUpdateDto);
            if (!validationResult.IsValid)
            {
                return ResultViewModel<UserUpdateDto>.Failure("Please check credentials.", validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var userEntity = await _baseRepository.GetAsync(filter: e => e.Id == id, include: q => q.Include(e => e.UserRoles).ThenInclude(e => e.Role));
            if (userEntity == null)
            {
                return ResultViewModel<UserUpdateDto>.Failure("User not found.", null, 404);
            }
            _mapper.Map(userUpdateDto, userEntity);
            if (!string.IsNullOrEmpty(userUpdateDto.Password))
            {
                userEntity.Password = _hashingHelper.HashPassword(userUpdateDto.Password);
            }
            userEntity.UserRoles.Clear();
            userEntity.UserRoles.Add(new UserRole
            {
                RoleId = userUpdateDto.RoleId,
                UserId = id
            });
            await _baseRepository.UpdateAsync(userEntity);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<UserUpdateDto>.Success("User updated succesfully", 200);
        }

    }
}
