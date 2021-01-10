using Catalogue.API.Infrastructure;
using Catalogue.UnitTests.Fixtures;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Catalogue.UnitTests.Configuration
{
    public abstract class CatalogueIntegrationTest
    {
        protected TestServer TestServer { get; set; }

        protected void ResetDatabase(Dataset dataset = Dataset.GroupByFamilles)
        {
            IServiceProvider iServiceProvider = TestServer.Host.Services;

            using IServiceScope scope = iServiceProvider.CreateScope();
            CatalogueContext db = scope.ServiceProvider.GetRequiredService<CatalogueContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            switch (dataset)
            {
                case Dataset.GroupByFamilles:
                    {
                        SeedData.PopulateTestDataForGroupByFamilles(db);
                        break;
                    }
            }
        }
    }

    public enum Dataset
    {
        GroupByFamilles
    }
}
