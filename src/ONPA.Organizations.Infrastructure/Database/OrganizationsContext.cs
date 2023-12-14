using MediatR;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Infrastructure;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Infrastructure.Database;

public class OrganizationsContext : StreamContext
{
    public OrganizationsContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
    {
    }
    
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationFee> OrganizationFees { get; set; }
    public DbSet<OrganizationSettings> OrganizationSettings { get; set; }
}