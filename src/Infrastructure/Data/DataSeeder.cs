using Bogus;
using CrudProducts.Properties.Domain.Models;
using CrudProducts.Properties.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace CrudProducts.src.Infrastructure.Data
{
    public class DataSeeder
    {
         public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                var logger = scope.ServiceProvider.GetRequiredService<ILogger<DataSeeder>>();
                try
                {
                    try {
                        await context.Database.MigrateAsync();
                    }
                        catch (MySqlException ex) when (ex.SqlState == "42P07") {
                            logger.LogWarning("Algunas tablas ya existen en la base de datos MySQL: {Message}", ex.Message);
                    }
                    if(!await context.Products.AnyAsync())
                    {
                        var faker = new Faker<Product>("es")
                            .RuleFor(p => p.SKU, f => f.Commerce.Ean13().ToString())
                            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                            .RuleFor(p => p.Price, f => f.Random.Int(1, 100000))
                            .RuleFor(p => p.Stock, f => f.Random.Int(1, 1000))
                            .RuleFor(p => p.IsActive, f => f.Random.Bool());
                        var products = faker.Generate(100);
                        context.Products.AddRange(products);
                        await context.SaveChangesAsync();
                    }
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, "Un error ha ocurrido mientras se cargaban los seeders");
                }
            }
        }
    }
}