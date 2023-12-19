using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Gatherings.Domain;
using ONPA.Gatherings.Infrastructure.Database;
using ONPA.Gatherings.Infrastructure.Repositories;
using ONPA.IntegrationEventsLog.Services;

namespace ONPA.Gatherings.Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage(Justification = "Tested through integration tests")]
public static class ServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMultiTenantAggregateRepository<Event>, EventAggregateRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.Event>, EventReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.EventEnrollment>, EventEnrollmentReadOnlyRepository>();
        services.AddDbContext<GatheringsContext>();
        services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<GatheringsContext>>();
       
        return services;
    }
    
}