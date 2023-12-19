using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using ONPA.Organizations.Domain;
using ONPA.Organizations.Infrastructure.Database;

namespace ONPA.Organizations.Infrastructure.DataSeed;

public partial class OrganizationSeed(ILogger<OrganizationSeed> logger) : IDbSeeder<OrganizationsContext>
{
    public async Task SeedAsync(OrganizationsContext context)
    {
        context.Database.OpenConnection();
        ((NpgsqlConnection)context.Database.GetDbConnection()).ReloadTypes();
        
        if (!await context.Organizations.AnyAsync())
        {
            var organizationAggregate = new Organization(Guid.NewGuid(),
                "Organizacja Demonstracyjna", "demo", "PL1112233444", "PL00111122223333444455556666",
                "Adres","adres@email.com");
            await context.StoreAggregate(organizationAggregate, new CancellationToken());
            
            await context.SaveChangesAsync();
        }
    }
}