using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Infrastructure;
using ONPA.IntegrationEventsLog;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Infrastructure.Database;

public class MembershipContext : StreamContext
{
    public MembershipContext(DbContextOptions<MembershipContext> options, IMediator mediator) : base(options, mediator)
    {
    }
    
    public DbSet<Member> Members { get; set; } 
    public DbSet<MemberFee> MemberFees { get; set; }
    public DbSet<MemberSuspension> MemberSuspensions { get; set; }
    public DbSet<MemberExpel> MemberExpels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MembershipContext).Assembly);
        modelBuilder.UseStreamModels("membership");
        modelBuilder.UseIntegrationEventLogs("membership");
    }
}