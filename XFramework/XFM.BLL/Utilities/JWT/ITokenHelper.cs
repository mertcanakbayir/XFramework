using Dtos;

namespace XFM.BLL.Utilities.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(CreateTokenDto createTokenDto);
    }
}
