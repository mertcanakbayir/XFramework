using Microsoft.Extensions.Options;

namespace XFramework.Extensions.Configurations.ConfigurationValidations
{
    public class CacheOptionsValidator : IValidateOptions<CacheOptions>
    {
        public ValidateOptionsResult Validate(string? name, CacheOptions options)
        {
            var errors = new List<string>();

            if (options.UserPageCacheMinutes < 0)
                errors.Add("Cache:UserPageCacheMinutes must be >= 0.");

            if (options.UserEndpointCacheMinutes < 0)
                errors.Add("Cache:UserEndpointCacheMinutes must be >= 0.");

            return errors.Any()
                ? ValidateOptionsResult.Fail(errors)
                : ValidateOptionsResult.Success;
        }
    }
}
