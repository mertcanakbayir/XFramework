using Microsoft.Extensions.Options;

namespace XFramework.Extensions.Configurations.ConfigurationValidations
{
    internal class EncryptionOptionsValidator : IValidateOptions<EncryptionOptions>
    {
        public ValidateOptionsResult Validate(string? name, EncryptionOptions options)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(options.Key))
                errors.Add("Encryption:Key is required.");

            if (options.Key != null && options.Key.Length < 16)
                errors.Add("Encryption:Key must be at least 16 chars.");

            if (string.IsNullOrWhiteSpace(options.IV))
                errors.Add("Encryption:IV is required.");

            if (options.IV != null && options.IV.Length < 16)
                errors.Add("Encryption:IV must be at least 16 chars.");

            return errors.Any()
                ? ValidateOptionsResult.Fail(errors)
                : ValidateOptionsResult.Success;
        }
    }
}
