using Catalogue.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalogue.API.Infrastructure
{
    public class CatalogueContext : DbContext
    {
        public CatalogueContext(DbContextOptions<CatalogueContext> options)
              : base(options)
        {
        }

        public DbSet<Famille> Familles { get; set; }
        public DbSet<Produit> Produits { get; set; }
    }
}
