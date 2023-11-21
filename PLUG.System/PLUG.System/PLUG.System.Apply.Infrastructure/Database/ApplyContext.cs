using MediatR;
using Microsoft.EntityFrameworkCore;
using PLUG.System.Common.Infrastructure;
using RecommendationRead = PLUG.System.Apply.Infrastructure.ReadModel.Recommendation;
using ApplicationFormRead = PLUG.System.Apply.Infrastructure.ReadModel.ApplicationForm;

namespace PLUG.System.Apply.Infrastructure.Database;

public class ApplyContext :StreamContext
{
    public ApplyContext(DbContextOptions<ApplyContext> options, IMediator mediator) : base(options,mediator)
    {
    }
    
    public DbSet<RecommendationRead> Recommendations { get; set; }
    public DbSet<ApplicationFormRead> ApplicationForms { get; set; }
    
}


