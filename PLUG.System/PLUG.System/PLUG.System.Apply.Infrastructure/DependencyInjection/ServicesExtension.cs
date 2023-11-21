using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PLUG.System.Apply.Domain;
using PLUG.System.Apply.Infrastructure.Database;
using PLUG.System.Apply.Infrastructure.Repositories;
using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using RecommendationRead = PLUG.System.Apply.Infrastructure.ReadModel.Recommendation;
using ApplicationFormRead = PLUG.System.Apply.Infrastructure.ReadModel.ApplicationForm;

namespace PLUG.System.Apply.Infrastructure.DependencyInjection;

public static class ServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAggregateRepository<ApplicationForm>, ApplicationAggregateRepository>();
        services.AddTransient<IReadOnlyRepository<RecommendationRead>, RecommendationReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ApplicationFormRead>, ApplicationFormReadOnlyRepository>();
        services.AddDbContext<ApplyContext>();
        return services;
    }
    
}