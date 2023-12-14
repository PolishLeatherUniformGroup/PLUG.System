 using Microsoft.EntityFrameworkCore;
 using Microsoft.EntityFrameworkCore.Metadata.Builders;
 using Microsoft.EntityFrameworkCore.Storage;
 using Microsoft.Extensions.DependencyInjection;
 using ONPA.IntegrationEventsLog.Services;

 namespace ONPA.IntegrationEventsLog;

public static class IntegrationLogExtensions
{
    public static void UseIntegrationEventLogs(this ModelBuilder builder, string schema)
    {
        builder.Entity<IntegrationEventLogEntry>(builder =>
        {
            builder.ToTable("IntegrationEventLog", schema: schema);

            builder.HasKey(e => e.EventId);
        });
    }
    
}