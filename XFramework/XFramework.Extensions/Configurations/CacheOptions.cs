namespace XFramework.Extensions.Configurations
{
    public class CacheOptions
    {
        public int UserPageCacheMinutes { get; set; }
        public int UserEndpointCacheMinutes { get; set; }
        public bool UseDistributed { get; set; }
    }
}
