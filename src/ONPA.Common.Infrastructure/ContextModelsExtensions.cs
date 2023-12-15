using Microsoft.EntityFrameworkCore;
using ONPA.Common.Infrastructure;

namespace Microsoft.AspNetCore.Hosting;

public static class ContextModelsExtensions
{
    public static void UseStreamModels(this ModelBuilder builder, string schema)
    {
        builder.Entity<StateEventLogEntry>(builder =>
        {
            builder.ToTable("AggregateStream", schema: schema);

            builder.HasKey(e => e.EventId);
        });
    }
}