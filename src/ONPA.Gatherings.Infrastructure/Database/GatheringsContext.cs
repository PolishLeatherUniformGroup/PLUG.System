using MediatR;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Infrastructure;

namespace ONPA.Gatherings.Infrastructure.Database;

public class GatheringsContext : StreamContext
{
    protected GatheringsContext(IMediator mediator) : base(mediator)
    {
    }

    public GatheringsContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
    {
    }
}