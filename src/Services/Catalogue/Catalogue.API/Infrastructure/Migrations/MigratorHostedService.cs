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
            if (!AppSettings.TEST_ENVIRONMENT.Equals(webHostEnvironment.EnvironmentName, StringComparison.OrdinalIgnoreCase))
            {
                using IServiceScope scope = serviceProvider.CreateScope();
                CatalogueContext catalogueContext = scope.ServiceProvider.GetRequiredService<CatalogueContext>();

                await catalogueContext.Database.MigrateAsync();

                if (!await catalogueContext.Produits.AnyAsync())
                {
                    catalogueContext.AddRange(BuildFamilles());
                    await catalogueContext.SaveChangesAsync();
                }
            }
        }
        private IEnumerable<Famille> BuildFamilles()
        {
            return null;
            //string[] logins = new[] { "Odinnou", "Cgt", "Tazacban", "Dashell", "Cwep", "16ar" };

            //Faker<ChatEntry> faker = new Faker<ChatEntry>()
            //                .RuleFor(entry => entry.DateCreated, fake => fake.Date.Past())
            //                .RuleFor(entry => entry.Message, fake => fake.Lorem.Sentences(2))
            //                .RuleFor(entry => entry.Login, fake => fake.PickRandom(logins));

            //return faker.Generate(2000);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
