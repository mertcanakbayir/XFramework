namespace XFramework.Extensions.Configurations
{
    public class CorsOptions
    {
        public string PolicyName { get; set; }
        public string[] AllowedOrigins { get; set; }
        public bool AllowCredentials { get; set; }
    }
}
