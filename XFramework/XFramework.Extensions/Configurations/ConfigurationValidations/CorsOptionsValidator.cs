using Microsoft.Extensions.Options;

namespace XFramework.Extensions.Configurations.ConfigurationValidations
{
    public class CorsOptionsValidator : IValidateOptions<CorsOptions>
    {
        public ValidateOptionsResult Validate(string? name, CorsOptions options)
        {
            var errors = new List<string>();

            if (options.PolicyName == null || options.PolicyName.Length == 0)
            {
                errors.Add("Cors:PolicyName cannot be null");
            }

            if (options.AllowedOrigins == null || options.AllowedOrigins.Length == 0)
                errors.Add("Cors:AllowedOrigins must contain at least one origin.");

            if (options.AllowedOrigins != null)
            {
                foreach (var origin in options.AllowedOrigins)
                {
                    if (string.IsNullOrWhiteSpace(origin))
                        errors.Add("Cors:AllowedOrigins cannot contain empty values.");
                }
            }

            return errors.Any()
                ? ValidateOptionsResult.Fail(errors)
                : ValidateOptionsResult.Success;
        }
    }
}
