using Dtos;
using XFramework.Dtos;
using XFramework.Dtos.User;

namespace XFramework.BLL.Utilities.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(CreateTokenDto createTokenDto);

        PasswordResetTokenDto CreatePasswordResetToken(UserDto userDto);

        PasswordResetTokenDto CreateFirstTimeLoginToken(UserDto userDto);

        bool ValidatePasswordResetToken(string token, UserDto userDto, string tokenType);

    }
}
