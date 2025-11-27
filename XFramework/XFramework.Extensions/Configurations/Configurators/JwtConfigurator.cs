using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace XFramework.Extensions.Configurations.Configurators
{
    public class JwtConfigurator : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtOptions _jwt;
        public JwtConfigurator(IOptions<JwtOptions> jwt)
        {
            _jwt = jwt.Value;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            Configure(options);
        }

        public void Configure(JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = _jwt.Issuer,
                ValidAudience = _jwt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwt.Key)
                ),

                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
