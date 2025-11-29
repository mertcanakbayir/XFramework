using Microsoft.Extensions.Options;

namespace XFramework.Extensions.Configurations.ConfigurationValidations
{
    internal class RateLimitOptionsValidator : IValidateOptions<RateLimitOptions>
    {
        public ValidateOptionsResult Validate(string? name, RateLimitOptions options)
        {
            var errors = new List<string>();

            if (options.IpPermitLimit < 1)
                errors.Add("RateLimit:IpPermitLimit must be > 0");

            if (options.UserPermitLimit < 1)
                errors.Add("RateLimit:UserPermitLimit must be > 0");

            return errors.Any()
                ? ValidateOptionsResult.Fail(errors)
                : ValidateOptionsResult.Success;
        }
    }
}
