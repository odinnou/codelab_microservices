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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Famille>();
        }

        public DbSet<Produit> Produits { get; set; }
    }
}
