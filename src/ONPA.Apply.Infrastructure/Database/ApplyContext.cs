using MediatR;
using Microsoft.EntityFrameworkCore;
using ONPA.Apply.Infrastructure.ReadModel;
using ONPA.Common.Infrastructure;

namespace ONPA.Apply.Infrastructure.Database;

public class ApplyContext :StreamContext
{
    public ApplyContext(DbContextOptions<ApplyContext> options, IMediator mediator) : base(options,mediator)
    {
    }
    
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<ApplicationForm> ApplicationForms { get; set; }
    
}


