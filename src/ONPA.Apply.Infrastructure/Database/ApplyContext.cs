using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ONPA.Apply.Infrastructure.ReadModel;
using ONPA.Common.Infrastructure;
using Npgsql;
using ONPA.IntegrationEventsLog;

namespace ONPA.Apply.Infrastructure.Database;

[ExcludeFromCodeCoverage(Justification = "Tested through integration tests")]
public class ApplyContext :StreamContext
{
  
    public ApplyContext() : base(mediator:null)
    {
    }

    public ApplyContext(DbContextOptions<ApplyContext> options, IMediator mediator) : base(options,mediator)
    {
    }

    
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<ApplicationForm> ApplicationForms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.HasPostgresExtension("vector");
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplyContext).Assembly);
       modelBuilder.UseIntegrationEventLogs("apply");
       modelBuilder.UseStreamModels("apply");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            var connectionString = config.GetConnectionString("ApplyDB");
            var builder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Password = config["ApplyDBPassword"]
            };
            optionsBuilder.UseNpgsql(builder.ToString());
        }
    }
}


