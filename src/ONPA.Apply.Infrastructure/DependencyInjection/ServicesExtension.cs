using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONPA.Apply.Infrastructure.Database;
using ONPA.Apply.Infrastructure.Repositories;
using PLUG.System.Apply.Domain;
using ONPA.Common.Application;
using ONPA.Common.Domain;
using RecommendationRead = ONPA.Apply.Infrastructure.ReadModel.Recommendation;
using ApplicationFormRead = ONPA.Apply.Infrastructure.ReadModel.ApplicationForm;
using Recommendation = ONPA.Apply.Infrastructure.ReadModel.Recommendation;

namespace ONPA.Apply.Infrastructure.DependencyInjection;

public static class ServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAggregateRepository<ApplicationForm>, ApplicationAggregateRepository>();
        services.AddTransient<IReadOnlyRepository<Recommendation>, RecommendationReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.ApplicationForm>, ApplicationFormReadOnlyRepository>();
        services.AddDbContext<ApplyContext>();
       
        return services;
    }
    
}