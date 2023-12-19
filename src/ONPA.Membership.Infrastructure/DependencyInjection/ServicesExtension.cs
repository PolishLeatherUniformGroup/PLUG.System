using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.IntegrationEventsLog.Services;
using ONPA.Membership.Domain;
using ONPA.Membership.Infrastructure.Database;
using ONPA.Membership.Infrastructure.Repositories;
using ReadModel = ONPA.Membership.Infrastructure.ReadModel;


namespace ONPA.Organizations.Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage(Justification = "Tested through integration tests")]
public static class ServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMultiTenantAggregateRepository<Member>, MemberAggregateRepository>();
        services.AddTransient<IMultiTenantAggregateRepository<MembersGroup>, MemberGroupAggregateRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.Member>, MemberReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.MemberFee>, MemberFeeReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.MemberExpel>, MemberExpelReadOnlyRepository>();
        services.AddTransient<IReadOnlyRepository<ReadModel.MemberSuspension>, MemberSuspensionReadOnlyRepository>();
        services.AddDbContext<MembershipContext>();
        services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<MembershipContext>>();
        return services;
    }
}