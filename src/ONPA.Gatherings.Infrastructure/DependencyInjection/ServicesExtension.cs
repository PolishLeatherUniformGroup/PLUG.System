using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Gatherings.Domain;
using ONPA.Gatherings.Infrastructure.Database;
using ONPA.Gatherings.Infrastructure.Repositories;
using ONPA.IntegrationEventsLog.Services;

namespace ONPA.Gatherings.Infrastructure.DependencyInjection;

public static class ServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAggregateRepository<Event>, PublicGatheringAggregateRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.Event>, PublicGatheringReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.EventEnrollment>, GatheringEnrollmentReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.EventParticipant>, GatheringParticipantReadOnlyRepository>();
        services.AddDbContext<GatheringsContext>();
        services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<GatheringsContext>>();
       
        return services;
    }
    
}