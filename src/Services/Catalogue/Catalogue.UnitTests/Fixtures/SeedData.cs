using Catalogue.API.Infrastructure;

namespace Catalogue.UnitTests.Fixtures
{
    public static class SeedData
    {
        public static void PopulateTestDataForGroupByFamilles(CatalogueContext catalogueContext)
        {
            catalogueContext.AddRange(API.Infrastructure.Migrations.MigratorHostedService.BuildProduits());
            catalogueContext.SaveChanges();
        }
    }
}
