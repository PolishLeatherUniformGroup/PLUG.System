using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Infrastructure.ReadModel;
using ONPA.IntegrationEventsLog;

namespace ONPA.Gatherings.Infrastructure.Database;

[ExcludeFromCodeCoverage(Justification = "Tested through integration tests")]
public class GatheringsContext : StreamContext
{
    public GatheringsContext() : base(mediator:null)
    {
    }
#if !LOCAL
    public GatheringsContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
    {
    }
    #endif
    public DbSet<Event> Events { get; set; }
    public DbSet<EventEnrollment> EventEnrollments { get; set; }
    public DbSet<EventParticipant> EventParticipants { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
#if LOCAL
        modelBuilder.HasPostgresExtension("vector");
#endif
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GatheringsContext).Assembly);
        modelBuilder.UseStreamModels("gatherings");
        modelBuilder.UseIntegrationEventLogs("gatherings");
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
                x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "gatherings");
            });
            
        }
#else

        optionsBuilder.UseNpgsql(x =>
        {
            x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "gatherings");
        });
#endif

    }
}