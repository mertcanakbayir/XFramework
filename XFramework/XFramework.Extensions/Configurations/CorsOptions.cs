namespace XFramework.Extensions.Configurations
{
    public class CorsOptions
    {
        public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
        public bool AllowCredentials { get; set; } = false;
    }
}
