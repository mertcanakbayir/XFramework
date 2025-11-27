using Microsoft.Extensions.Options;

namespace XFramework.Extensions.Configurations.ConfigurationValidations
{
    public class JwtOptionsValidator : IValidateOptions<JwtOptions>
    {
        public ValidateOptionsResult Validate(string? name, JwtOptions options)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(options.Issuer))
                errors.Add("Jwt:Issuer is required.");

            if (string.IsNullOrWhiteSpace(options.Audience))
                errors.Add("Jwt:Audience is required.");

            if (string.IsNullOrWhiteSpace(options.Key))
                errors.Add("Jwt:Key is required.");

            if (options.Key != null && options.Key.Length < 16)
                errors.Add("Jwt:Key must be at least 16 characters.");

            return errors.Any()
                ? ValidateOptionsResult.Fail(errors)
                : ValidateOptionsResult.Success;
        }
    }
}
