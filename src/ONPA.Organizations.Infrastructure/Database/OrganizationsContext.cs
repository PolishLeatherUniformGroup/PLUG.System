using MediatR;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Infrastructure;

namespace ONPA.Organizations.Infrastructure.Database;

public class OrganizationsContext : StreamContext
{
    public OrganizationsContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
    {
    }
}