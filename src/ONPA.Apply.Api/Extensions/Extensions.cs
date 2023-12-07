using FluentValidation;
using ONPA.Apply.Api.Application.Behavior;
using ONPA.Apply.Api.Application.IntegrationEvents;
using ONPA.Apply.Api.Application.IntegrationEvents.EventHandlers;
using ONPA.Apply.Api.Services;
using PLUG.System.Apply.Domain;
using ONPA.Apply.Infrastructure.Database;
using ONPA.Apply.Infrastructure.Repositories;
using ONPA.Common.Application;
using ONPA.Common.Behaviors;
using ONPA.Common.Domain;
using ONPA.EventBus.Abstraction;
using ONPA.IntegrationEvents;
using ONPA.IntegrationEventsLog.Services;
using ONPA.ServiceDefaults;
using RecommendationRead = ONPA.Apply.Infrastructure.ReadModel.Recommendation;
using ApplicationFormRead = ONPA.Apply.Infrastructure.ReadModel.ApplicationForm;

namespace ONPA.Apply.Api.Extensions;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // Add the authentication services to DI
        builder.AddDefaultAuthentication();

        // Pooling is disabled because of the following error:
        // Unhandled exception. System.InvalidOperationException:
        // The DbContext of type 'OrderingContext' cannot be pooled because it does not have a public constructor accepting a single parameter of type DbContextOptions or has more than one constructor.
        builder.AddNpgsqlDbContext<ApplyContext>("ApplyDB", settings => settings.DbContextPooling = false);

        //builder.Services.AddMigration<ApplyContext, ApplyContextSeed>();

        // Add the integration services that consume the DbContext
        builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<ApplyContext>>();

        builder.Services.AddTransient<IIntegrationEventService, IntegrationEventService>();

        builder.Services.AddTransient<IAggregateRepository<ApplicationForm>, ApplicationAggregateRepository>();
        builder.Services.AddTransient<IReadOnlyRepository<RecommendationRead>, RecommendationReadOnlyRepository>();
        builder.Services.AddTransient<IReadOnlyRepository<ApplicationFormRead>, ApplicationFormReadOnlyRepository>();

        builder.AddRabbitMqEventBus("EventBus")
            .AddEventBusSubscriptions();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IIdentityService, IdentityService>();

        // Configure mediatR
        var services = builder.Services;

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

            cfg.AddOpenBehavior(typeof(CommandLoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionalBehavior<,>));
        });

        // Register the command validators for the validator behavior (validators based on FluentValidation library)
        services.AddValidatorsFromAssemblyContaining<ApplicationForm>();
    }
    
    private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
    {
        eventBus.AddSubscription<ApplicationRecommendersValidatedIntegrationEvent, ApplicationRecommendersValidatedIntegrationEventHandler>();
    }
    
}