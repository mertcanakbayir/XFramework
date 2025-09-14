using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using XFramework.Dtos;
using XFramework.Dtos.User;

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
                new Claim(JwtRegisteredClaimNames.PreferredUsername,createTokenDto.Username),
            };

            foreach (var role in createTokenDto.Role)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

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

        public PasswordResetTokenDto CreatePasswordResetToken(UserDto userDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Sub,userDto.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,userDto.Email),
                new Claim("TokenType","ResetToken")
            };

            var tokenExpiry = DateTime.UtcNow.AddMinutes(15);
            var token = new JwtSecurityToken(
           issuer: _jwtSettings.Issuer,
           audience: _jwtSettings.Audience,
           claims: claims,
           expires: tokenExpiry,
           signingCredentials: credentials
       );

            return new PasswordResetTokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = tokenExpiry
            };
        }

        public bool ValidatePasswordResetToken(string token, UserDto userDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var tokenType = principal.Claims.FirstOrDefault(c => c.Type == "TokenType")?.Value;

                if (tokenType != "ResetToken")
                {
                    return false;
                }

                var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (email != userDto.Email)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
