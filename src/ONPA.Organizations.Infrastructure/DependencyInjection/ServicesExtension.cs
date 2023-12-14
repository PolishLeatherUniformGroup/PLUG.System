using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Organizations.Domain;
using ONPA.Organizations.Infrastructure.Database;
using ONPA.Organizations.Infrastructure.Repositories;
using ReadModel = ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Infrastructure.DependencyInjection;

public static class ServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAggregateRepository<Organization>, OrganizationAggregateRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.Organization>, OrganizationReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.OrganizationFee>, OrganizationFeeReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.OrganizationSettings>, OrganizationSettingsReadOnlyRepository>();
        services.AddDbContext<OrganizationsContext>();
        return services;
    }
}