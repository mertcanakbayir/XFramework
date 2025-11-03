namespace XFramework.Configuration
{
    public class RateLimitOptions
    {
        public int IpPermitLimit { get; set; } = 20;
        public int IpWindowMinutes { get; set; } = 1;
        public int UserPermitLimit { get; set; } = 50;
        public int UserWindowMinutes { get; set; } = 1;
        public bool EnableRateLimiting { get; set; } = true;
    }
}
