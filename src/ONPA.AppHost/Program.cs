using ONPA.Apply.Infrastructure.Database;

var builder = DistributedApplication.CreateBuilder(args);

var rabbitMq = builder.AddRabbitMQContainer("EventBus");

var postgres = builder.AddPostgresContainer("postgres")
    .WithAnnotation(new ContainerImageAnnotation
    {
        Image = "ankane/pgvector",
        Tag = "latest"
    }).WithEnvironment("POSTGRES_DB", "onpa_db");
var database = postgres.AddDatabase("onpa_db");

var identityApi = builder.AddProject<Projects.ONPA_Identity_Api>("identity-api")
    .WithReference(database)
    .WithLaunchProfile("https");

var applyApi = builder.AddProject<Projects.ONPA_Apply_Api>("apply-api")
    .WithReference(database)
    .WithReference(rabbitMq)
    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

var membershipApi = builder.AddProject<Projects.ONPA_Membership_Api>("membership-api")
    .WithReference(database)
    .WithReference(rabbitMq)
    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

var gatheringsApi = builder.AddProject<Projects.ONPA_Gatherings_Api>("gathering-api")
    .WithReference(database)
    .WithReference(rabbitMq)
    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

var organizationApi = builder.AddProject<Projects.ONPA_Organizations_Api>("organization-api")
    .WithReference(database)
    .WithReference(rabbitMq)
    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

var communictationApi = builder.AddProject<Projects.ONPA_Communication_Api>("communication-api")
    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

var webapp = builder.AddProject<Projects.ONPA_WebApp>("webapp")
    .WithReference(applyApi)
    .WithReference(membershipApi)
    .WithReference(gatheringsApi)
    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

webapp.WithEnvironmentForServiceBinding("CallBackUrl", webapp, bindingName: "https")
    .WithEnvironmentForServiceBinding("Services__ApplyService__Uri", applyApi, "https")
    .WithEnvironmentForServiceBinding("Services__MembershipService__Uri", membershipApi, "https")
    .WithEnvironmentForServiceBinding(name: "Services__OrganizationService__Uri", organizationApi,
        bindingName: "https");


identityApi
    .WithEnvironmentForServiceBinding("ApplyApiClient", applyApi, bindingName: "https")
    .WithEnvironmentForServiceBinding("WebAppClient", webapp, bindingName: "https");


builder.Build().Run();