using MediatR;
using Microsoft.EntityFrameworkCore;
using ONPA.Apply.Infrastructure.ReadModel;
using ONPA.Common.Infrastructure;
using RecommendationRead = ONPA.Apply.Infrastructure.ReadModel.Recommendation;
using ApplicationFormRead = ONPA.Apply.Infrastructure.ReadModel.ApplicationForm;

namespace ONPA.Apply.Infrastructure.Database;

public class ApplyContext :StreamContext
{
    public ApplyContext(DbContextOptions<ApplyContext> options, IMediator mediator) : base(options,mediator)
    {
    }
    
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<ApplicationForm> ApplicationForms { get; set; }
    
}


