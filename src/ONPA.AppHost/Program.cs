var builder = DistributedApplication.CreateBuilder(args);

var rabbitMq = builder.AddRabbitMQContainer("EventBus");

var postgres = builder.AddPostgresContainer("postgres")
    .WithAnnotation(new ContainerImageAnnotation
    {
        Image = "postgres",
        Tag = "14-bullseye"
    });
var applyDb = postgres.AddDatabase("ApplyDB");
var membershipDb = postgres.AddDatabase("MembershipDB");
var gatheringDb = postgres.AddDatabase("GatheringDB");
var identityDb = postgres.AddDatabase("IdentityDB");

 var identityApi = builder.AddProject<Projects.ONPA_Identity_Api>("identity-api")
     .WithReference(identityDb)
     .WithLaunchProfile("https");

 var applyApi = builder.AddProject<Projects.ONPA_Apply_Api>("apply-api")
    .WithReference(applyDb)
    .WithReference(rabbitMq)
    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

var membershipApi = builder.AddProject<Projects.ONPA_Membership_Api>("membership-api")
    .WithReference(membershipDb)
    .WithReference(rabbitMq)
    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

var gatheringsApi = builder.AddProject<Projects.ONPA_Gatherings_Api>("gathering-api")
    .WithReference(gatheringDb)
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

webapp.WithEnvironmentForServiceBinding("CallBackUrl", webapp, bindingName: "https");

identityApi
    .WithEnvironmentForServiceBinding("ApplyApiClient", applyApi, bindingName:"https")
    .WithEnvironmentForServiceBinding("WebAppClient", webapp, bindingName: "https");




builder.Build().Run();