using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ONPA.Common.Infrastructure;
using ONPA.IntegrationEventsLog;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Infrastructure.Database;

[ExcludeFromCodeCoverage(Justification = "Tested through integration tests")]
public class MembershipContext : StreamContext
{
    public MembershipContext():base(mediator:null)
    {
        
    }
#if !LOCAL
    public MembershipContext(DbContextOptions<MembershipContext> options, IMediator mediator) : base(options, mediator)
    {
    }
#endif

    public DbSet<Member> Members { get; set; } 
    public DbSet<MemberFee> MemberFees { get; set; }
    public DbSet<MemberSuspension> MemberSuspensions { get; set; }
    public DbSet<MemberExpel> MemberExpels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MembershipContext).Assembly);
        modelBuilder.UseStreamModels("membership");
        modelBuilder.UseIntegrationEventLogs("membership");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if LOCAL
        if (!optionsBuilder.IsConfigured)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            var connectionString = config.GetConnectionString("onpa_db");
            var builder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Password = config["ApplyDBPassword"],
            };
            optionsBuilder.UseNpgsql(builder.ToString(), x =>
            {
                x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "membership");
            });
            
        }
#else

        optionsBuilder.UseNpgsql(x =>
        {
            x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "membership");
        });
#endif
        
    }
}