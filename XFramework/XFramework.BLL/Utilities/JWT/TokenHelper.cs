using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using XFramework.Dtos;
using XFramework.Dtos.User;
using XFramework.Extensions.Configurations;

namespace XFramework.BLL.Utilities.JWT
{
    public class TokenHelper : ITokenHelper
    {
        private readonly JwtOptions _jwt;
        public TokenHelper(IOptions<JwtOptions> jwtOptions)
        {
            _jwt = jwtOptions.Value;
        }

        public AccessToken CreateToken(CreateTokenDto createTokenDto)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
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

            var tokenExpiry = DateTime.UtcNow.AddMinutes(_jwt.ExpireInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
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
            return CreatePasswordResetToken(userDto, "PasswordReset");
        }
        public PasswordResetTokenDto CreateFirstTimeLoginToken(UserDto userDto)
        {
            return CreatePasswordResetToken(userDto, "FirstTimeLogin");
        }

        private PasswordResetTokenDto CreatePasswordResetToken(UserDto userDto, string tokenType)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                    {
                    new Claim(JwtRegisteredClaimNames.Sub,userDto.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,userDto.Email),
                    new Claim("TokenType",tokenType)
                };

            var tokenExpiry = DateTime.UtcNow.AddMinutes(15);
            var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
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

        public bool ValidatePasswordResetToken(string token, UserDto userDto, string tokenType)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwt.Issuer,
                ValidAudience = _jwt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var tokenTypeFromToken = principal.Claims.FirstOrDefault(c => c.Type == "TokenType")?.Value;

                if (tokenTypeFromToken != tokenType)
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
