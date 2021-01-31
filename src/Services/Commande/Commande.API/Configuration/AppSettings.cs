namespace Commande.API.Configuration
{
    public class AppSettings
    {
        public AuthorityConfiguration AuthorityConfiguration { get; set; }
        public KafkaConfiguration KafkaConfiguration { get; set; }
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

    public class KafkaConfiguration
    {
        public string Brokers { get; set; }
        public string ConsumerGroup { get; set; }
    }
}
