using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Infrastructure.Database;

[ExcludeFromCodeCoverage(Justification = "Tested through integration tests")]
public class GatheringsContext : StreamContext
{
    protected GatheringsContext(IMediator mediator) : base(mediator)
    {
    }

    public GatheringsContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
    {
    }
    
    public DbSet<Event> Events { get; set; }
    public DbSet<EventEnrollment> EventEnrollments { get; set; }
}