namespace XFramework.Extensions.Configurations
{
    public class RateLimitOptions
    {
        public int IpPermitLimit { get; set; }
        public int IpWindowMinutes { get; set; }
        public int UserPermitLimit { get; set; }
        public int UserWindowMinutes { get; set; }
        public bool EnableRateLimiting { get; set; }
    }
}
