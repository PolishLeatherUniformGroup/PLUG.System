using Microsoft.AspNetCore.Hosting;
using ONPA.Membership.Infrastructure.Database;

namespace ONPA.Membership.Infrastructure.DataSeed;

public class MembershipSeed : IDbSeeder<MembershipContext>
{
    public Task SeedAsync(MembershipContext context)
    {
        return Task.CompletedTask;
    }
}