namespace MyApp.Configuration
{
    public class CacheOptions
    {
        public int UserPageCacheMinutes { get; set; } = 30;
        public int UserEndpointCacheMinutes { get; set; } = 30;
        public bool UseDistributed { get; set; } = false;
    }
}
