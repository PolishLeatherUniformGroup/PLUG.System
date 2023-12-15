using MediatR;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Infrastructure.Database;

public class GatheringsContext : StreamContext
{
    protected GatheringsContext(IMediator mediator) : base(mediator)
    {
    }

    public GatheringsContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
    {
    }
    
    public DbSet<Event> PublicGatherings { get; set; }
    public DbSet<EventEnrollment> GatheringEnrollments { get; set; }
    public DbSet<EventParticipant> GatheringParticipants { get; set; }
}