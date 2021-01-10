namespace Catalogue.API.Infrastructure
{
    public abstract class BaseRepository
    {
        protected readonly CatalogueContext catalogueContext;

        protected BaseRepository(CatalogueContext catalogueContext)
        {
            this.catalogueContext = catalogueContext;
        }
    }
}
