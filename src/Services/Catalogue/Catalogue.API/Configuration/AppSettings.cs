using System.Collections.Generic;

namespace Catalogue.API.Configuration
{
    public class AppSettings
    {
        public const string TEST_ENVIRONMENT = "test";

        public string Title { get; set; }
        public string DbConnection { get; set; }
        public int Ttl { get; set; }
        public List<string> ApiKeys { get; set; }
        public List<ComplexItem> ComplexItems { get; set; }
    }

    public class ComplexItem
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
    }
}
