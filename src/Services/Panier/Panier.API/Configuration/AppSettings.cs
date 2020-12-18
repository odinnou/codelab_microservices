namespace Catalogue.API.Configuration
{
    public class AppSettings
    {
        public CacheConfiguration CacheConfiguration { get; set; }
    }

    public class CacheConfiguration
    {
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }
        public int TimeToLive { get; set; }
    }
}
