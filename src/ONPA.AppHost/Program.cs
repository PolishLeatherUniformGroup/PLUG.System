using ONPA.Apply.Infrastructure.Database;

var builder = DistributedApplication.CreateBuilder(args);

/*
var rabbitMq = builder.AddRabbitMQContainer("EventBus");
var postgres = builder.AddPostgresContainer("postgres")
    .WithAnnotation(new ContainerImageAnnotation
    {
        Image = "ankane/pgvector",
        Tag = "latest"
    }).WithEnvironment("POSTGRES_DB", "onpa_db");
var database = postgres.AddDatabase("onpa_db");
*/
var dbConnectionString =
    "Server=onpa.postgres.database.azure.com;Database=onpa_db;Port=5432;User Id=onpa_admin;Password=d!Zujocaci2401;Ssl Mode=Require;";

var eventBusConnectionString = "amqps://bfrclisr:eG46ejMI_FlPG5CB3KXrx48wfpzBVowA@goose.rmq2.cloudamqp.com/bfrclisr";


var identityApi = builder.AddProject<Projects.ONPA_Identity_Api>("identity-api")
    .WithEnvironment("Aspire__Npgsql__EntityFrameworkCore__PostgreSQL__ConnectionString", dbConnectionString)
    .WithEnvironment("ConnectionStrings__EventBus", eventBusConnectionString)

    .WithLaunchProfile("https");

var applyApi = builder.AddProject<Projects.ONPA_Apply_Api>("apply-api")
    .WithEnvironment("Aspire__Npgsql__EntityFrameworkCore__PostgreSQL__ConnectionString", dbConnectionString)
    .WithEnvironment("ConnectionStrings__EventBus", eventBusConnectionString)
    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

var membershipApi = builder.AddProject<Projects.ONPA_Membership_Api>("membership-api")

    .WithEnvironment("Aspire__Npgsql__EntityFrameworkCore__PostgreSQL__ConnectionString", dbConnectionString)
    .WithEnvironment("ConnectionStrings__EventBus", eventBusConnectionString)

    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

var gatheringsApi = builder.AddProject<Projects.ONPA_Gatherings_Api>("gathering-api")

    .WithEnvironment("Aspire__Npgsql__EntityFrameworkCore__PostgreSQL__ConnectionString", dbConnectionString)
    .WithEnvironment("ConnectionStrings__EventBus", eventBusConnectionString)

    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

var organizationApi = builder.AddProject<Projects.ONPA_Organizations_Api>("organization-api")

    .WithEnvironment("Aspire__Npgsql__EntityFrameworkCore__PostgreSQL__ConnectionString", dbConnectionString)
    .WithEnvironment("ConnectionStrings__EventBus", eventBusConnectionString)

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