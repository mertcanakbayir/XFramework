using AutoMapper;
using FluentValidation;
using XFramework.BLL.Utilities.Hashing;
using XFramework.DAL.Entities;
using XFramework.Dtos.User;
using XFramework.Helper.ViewModels;
using XFramework.Repository.Options;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IHashingHelper _hashingHelper;
        private readonly IValidator<UserAddDto> _userAddDtoValidator;
        private readonly IValidator<UserUpdateDto> _userUpdateDtoValidator;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IBaseRepository<User> userRepository, IMapper mapper, IHashingHelper hashingHelper,
            IValidator<UserAddDto> userAddDtoValidator,
            IValidator<UserUpdateDto> userUpdateDtoValidator, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _hashingHelper = hashingHelper;
            _userAddDtoValidator = userAddDtoValidator;
            _userUpdateDtoValidator = userUpdateDtoValidator;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultViewModel<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetAsync(new BaseRepoOptions<User>
            {
                Filter = e => e.Email == email
            });
            if (user == null)
            {
                return ResultViewModel<UserDto>.Failure("Kullanıcı bulunamadı", null, 404);
            }
            var userDto = _mapper.Map<UserDto>(user);
            return ResultViewModel<UserDto>.Success(userDto, "kullanıcı döndü", 200);
        }

        public async Task<ResultViewModel<UserDto>> GetUserById(int id)
        {
            var user = await _userRepository.GetAsync(new BaseRepoOptions<User>
            {
                Filter = e => e.Id == id
            });
            if (user == null)
            {
                return ResultViewModel<UserDto>.Failure("Kullanıcı bulunamadı", null, 404);
            }
            var userDto = _mapper.Map<UserDto>(user);
            return ResultViewModel<UserDto>.Success(userDto, "Başarılı", 200);
        }

        public async Task<PagedResultViewModel<UserDto>> GetUsers(int pageNumber = 1, int pageSize = 2)
        {
            var users = await _userRepository.GetAllAsync<UserDto>(new BaseRepoOptions<User>
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            if (!users.Any())
            {
                return PagedResultViewModel<UserDto>.Failure("Kullanıcı bulunamadı", null, 404);
            }

            var userDtos = _mapper.Map<List<UserDto>>(users);

            return PagedResultViewModel<UserDto>.Success(
           userDtos,
           totalCount: 9999,
           pageNumber: pageNumber,
           pageSize: pageSize,
           message: "Başarılı",
           statusCode: 200
   );
        }


        public async Task<ResultViewModel<UserAddDto>> AddUser(UserAddDto userAddDto)
        {
            var validationResult = _userAddDtoValidator.Validate(userAddDto);
            if (!validationResult.IsValid)
            {
                return ResultViewModel<UserAddDto>.Failure("Lütfen girdiğiniz bilgileri kontrol edin.", validationResult.Errors.Select(e => e.ErrorMessage).ToList(), 400);
            }
            var userEntity = _mapper.Map<User>(userAddDto);
            userEntity.Password = _hashingHelper.HashPassword(userEntity.Password);
            await _userRepository.AddAsync(userEntity);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<UserAddDto>.Success("Kullanıcı Başarıyla Eklendi", 201);
        }

        public async Task<ResultViewModel<UserUpdateDto>> UpdateUser(UserUpdateDto userUpdateDto, int id)
        {
            var validationResult = _userUpdateDtoValidator.Validate(userUpdateDto);
            if (!validationResult.IsValid)
            {
                return ResultViewModel<UserUpdateDto>.Failure("Lütfen Girdiğiniz bilgileri kontrol edin.", validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var userEntity = await _userRepository.GetAsync(new BaseRepoOptions<User> { Filter = e => e.Id == id });
            if (userEntity == null)
            {
                return ResultViewModel<UserUpdateDto>.Failure("Kullanıcı Bulunamadı", null, 404);
            }
            _mapper.Map(userUpdateDto, userEntity);
            if (!string.IsNullOrEmpty(userUpdateDto.Password))
            {
                userEntity.Password = _hashingHelper.HashPassword(userUpdateDto.Password);
            }
            await _userRepository.UpdateAsync(userEntity);
            return ResultViewModel<UserUpdateDto>.Success("Kullanıcı Güncellendi", 200);
        }

        public async Task<ResultViewModel<string>> DeleteUserById(int userId)
        {
            var user = await _userRepository.GetAsync(new BaseRepoOptions<User> { Filter = e => e.Id == userId });
            if (user == null)
            {
                return ResultViewModel<string>.Failure("Kullanıcı bulunamadı.", null, 404);
            }

            await _userRepository.DeleteAsync(userId);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<string>.Success("Kullanıcı silindi.", 200);
        }
    }
}
