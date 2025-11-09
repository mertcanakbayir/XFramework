using Dtos;
using MyApp.Dtos;
using MyApp.Dtos.User;

namespace MyApp.BLL.Utilities.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(CreateTokenDto createTokenDto);

        PasswordResetTokenDto CreatePasswordResetToken(UserDto userDto);

        bool ValidatePasswordResetToken(string token, UserDto userDto);

    }
}
