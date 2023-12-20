using Microsoft.AspNetCore.Hosting;
using ONPA.Gatherings.Infrastructure.Database;

namespace ONPA.Gatherings.Infrastructure.DataSeed;

public class GatheringsSeed : IDbSeeder<GatheringsContext>
{
    public Task SeedAsync(GatheringsContext context)
    {
        return Task.CompletedTask;
    }
}