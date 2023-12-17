using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Infrastructure;

namespace Microsoft.AspNetCore.Hosting;

[ExcludeFromCodeCoverage(Justification = "Tested via integration tests")]
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