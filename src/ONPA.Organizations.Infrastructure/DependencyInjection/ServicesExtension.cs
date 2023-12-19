using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.IntegrationEventsLog.Services;
using ONPA.Organizations.Domain;
using ONPA.Organizations.Infrastructure.Database;
using ONPA.Organizations.Infrastructure.Repositories;

namespace ONPA.Organizations.Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage(Justification = "Tested in integration tests")]
public static class ServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAggregateRepository<Organization>, OrganizationAggregateRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.Organization>, OrganizationReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.OrganizationFee>, OrganizationFeeReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.OrganizationSettings>, OrganizationSettingsReadOnlyRepository>();
        services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<OrganizationsContext>>();
        return services;
    }
}