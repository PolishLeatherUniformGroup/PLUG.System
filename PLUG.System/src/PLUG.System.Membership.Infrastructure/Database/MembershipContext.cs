using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Infrastructure;

namespace PLUG.System.Membership.Infrastructure.Database;

public class MembershipContext : StreamContext
{
    public MembershipContext(DbContextOptions<MembershipContext> options, IMediator mediator) : base(options, mediator)
    {
    }
}