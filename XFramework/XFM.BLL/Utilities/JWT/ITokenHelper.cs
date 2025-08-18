using Dtos;
using XFramework.Dtos;

namespace XFM.BLL.Utilities.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(CreateTokenDto createTokenDto);

        PasswordResetTokenDto CreatePasswordResetToken(UserDto userDto);

        bool ValidatePasswordResetToken(string token, UserDto userDto);

    }
}
