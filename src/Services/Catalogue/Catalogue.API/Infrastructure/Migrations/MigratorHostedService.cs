using Bogus;
using Catalogue.API.Configuration;
using Catalogue.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Catalogue.API.Infrastructure.Migrations
{
    public class MigratorHostedService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MigratorHostedService(IServiceProvider serviceProvider, IWebHostEnvironment webHostEnvironment)
        {
            this.serviceProvider = serviceProvider;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // En SQLITE il ne faut pas faire de MigrateAsync, si on veut être plus proche de la vérité il faut passer sur du TestContainer : https://github.com/HofmeisterAn/dotnet-testcontainers
            if (!AppSettings.TEST_ENVIRONMENT.Equals(webHostEnvironment.EnvironmentName, StringComparison.OrdinalIgnoreCase))
            {
                using IServiceScope scope = serviceProvider.CreateScope();
                CatalogueContext catalogueContext = scope.ServiceProvider.GetRequiredService<CatalogueContext>();

                await catalogueContext.Database.MigrateAsync(default);

                // Pour alimenter en fake data la démo
                if (!await catalogueContext.Familles.AnyAsync(default))
                {
                    catalogueContext.AddRange(BuildProduits());

                    await catalogueContext.SaveChangesAsync(default);
                }
            }
        }

        public static IEnumerable<Produit> BuildProduits()
        {
            IEnumerable<Famille> familles = new List<Famille> {
                new Famille { Nom = "Chaussures Homme" },
                new Famille { Nom = "Chaussures Femme" },
                new Famille { Nom = "Chaussures Enfant" },
                new Famille { Nom = "Vestes Homme" },
                new Famille { Nom = "Vestes Femme" },
                new Famille { Nom = "Vestes Enfant" },
                new Famille { Nom = "Manteaux Homme" },
                new Famille { Nom = "Manteaux Femme" },
                new Famille { Nom = "Manteaux Enfant" },
                new Famille { Nom = "Accessoires Homme" },
                new Famille { Nom = "Accessoires Femme" },
                new Famille { Nom = "Accessoires Enfant" }
            };

            return new Faker<Produit>()
                .RuleFor(famille => famille.Famille, fake => fake.PickRandom(familles))
                .RuleFor(famille => famille.Libelle, fake => fake.Commerce.ProductName())
                .RuleFor(famille => famille.Reference, fake => fake.Commerce.Ean13())
                .Generate(10000);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
