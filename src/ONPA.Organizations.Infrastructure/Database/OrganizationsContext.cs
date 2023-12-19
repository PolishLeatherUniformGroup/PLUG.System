using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ONPA.Common.Infrastructure;
using ONPA.IntegrationEventsLog;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Infrastructure.Database;

[ExcludeFromCodeCoverage(Justification = "Tested in integration tests")]
public class OrganizationsContext : StreamContext
{
    public OrganizationsContext() : base(mediator: null)
    {
    }

    public OrganizationsContext(DbContextOptions<OrganizationsContext> options, IMediator mediator) : base(options, mediator)
    {
    }

    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationFee> OrganizationFees { get; set; }
    public DbSet<OrganizationSettings> OrganizationSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrganizationsContext).Assembly);
        modelBuilder.UseIntegrationEventLogs("org");
        modelBuilder.UseStreamModels("org");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
}