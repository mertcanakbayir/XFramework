using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace XFramework.Extensions.Configurations.Configurators
{
    public class RateLimitConfigurator : IConfigureOptions<RateLimitOptions>
    {
        private readonly IConfiguration _config;

        public RateLimitConfigurator(IConfiguration config)
        {
            _config = config;
        }
        public void Configure(RateLimitOptions options)
        {
            _config.GetSection("RateLimit").Bind(options);
        }
    }
}
