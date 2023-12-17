using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONPA.Apply.Infrastructure.Database;
using ONPA.Apply.Infrastructure.Repositories;
using PLUG.System.Apply.Domain;
using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.IntegrationEventsLog.Services;
using Recommendation = ONPA.Apply.Infrastructure.ReadModel.Recommendation;

namespace ONPA.Apply.Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage(Justification = "Tested through integration tests")]
public static class ServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAggregateRepository<ApplicationForm>, ApplicationAggregateRepository>();
        services.AddTransient<IReadOnlyRepository<Recommendation>, RecommendationReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.ApplicationForm>, ApplicationFormReadOnlyRepository>();
        services.AddDbContext<ApplyContext>();
        services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<ApplyContext>>();
       
        return services;
    }
    
}