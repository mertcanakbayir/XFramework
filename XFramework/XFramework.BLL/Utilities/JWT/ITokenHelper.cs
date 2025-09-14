using Dtos;
using XFramework.Dtos;
using XFramework.Dtos.User;

namespace XFM.BLL.Utilities.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(CreateTokenDto createTokenDto);

        PasswordResetTokenDto CreatePasswordResetToken(UserDto userDto);

        bool ValidatePasswordResetToken(string token, UserDto userDto);

    }
}
