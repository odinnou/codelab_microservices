namespace Panier.API.Configuration
{
    public class AppSettings
    {
        public CacheConfiguration CacheConfiguration { get; set; }
        public AuthorityConfiguration AuthorityConfiguration { get; set; }
        public string RoutePrefix { get; set; }
        public bool EnableSwagger { get; set; }
        public string HttpRequestScheme { get; set; }
        public string DeployedVersion { get; set; }
    }

    public class AuthorityConfiguration
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
    }

    public class CacheConfiguration
    {
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }
        public int TimeToLive { get; set; }
    }
}
