using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace XFM.BLL.Utilities.JWT
{
    public class TokenHelper : ITokenHelper
    {
        private readonly JwtSettings _jwtSettings;
        public TokenHelper(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public AccessToken CreateToken(CreateTokenDto createTokenDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                  new Claim(JwtRegisteredClaimNames.Sub,createTokenDto.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, createTokenDto.Email),
                new Claim(JwtRegisteredClaimNames.PreferredUsername,createTokenDto.Username)
            };

            var tokenExpiry = DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: tokenExpiry,
                signingCredentials: credentials
                );

            return new AccessToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = tokenExpiry
            };

        }
    }
}
