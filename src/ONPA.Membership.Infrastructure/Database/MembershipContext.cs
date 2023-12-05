using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ONPA.Common.Domain;
using ONPA.Common.Infrastructure;

namespace ONPA.Membership.Infrastructure.Database;

public class MembershipContext : StreamContext
{
    public MembershipContext(DbContextOptions<MembershipContext> options, IMediator mediator) : base(options, mediator)
    {
    }
}