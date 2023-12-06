var builder = DistributedApplication.CreateBuilder(args);

var rabbitMq = builder.AddRabbitMQContainer("EventBus");
var postgres = builder.AddPostgresContainer("postgres")
    .WithAnnotation(new ContainerImageAnnotation
    {
        Image = "postgres",
        Tag = "14-bullseye"
    });
var applyDb = postgres.AddDatabase("ApplyDB");
var identityDb = postgres.AddDatabase("IdentityDB");

 var identityApi = builder.AddProject<Projects.ONPA_Identity_Api>("identity-api")
     .WithReference(identityDb);

 var applyApi = builder.AddProject<Projects.ONPA_Apply_Api>("apply-api")
    .WithReference(applyDb)
    .WithReference(rabbitMq)
    .WithEnvironmentForServiceBinding("Identity__Url", identityApi)
    .WithLaunchProfile("https");;
var membershipApi = builder.AddProject<Projects.ONPA_Membership_Api>("membership-api");
var gatheringsApi = builder.AddProject<Projects.ONPA_Gatherings_Api>("gathering-api");

var webapp = builder.AddProject<Projects.ONPA_WebApp>("webapp")
    .WithReference(applyApi)
    .WithReference(membershipApi)
    .WithEnvironmentForServiceBinding("IdentityUrl", identityApi)
    .WithLaunchProfile("https");

webapp.WithEnvironmentForServiceBinding("CallBackUrl", webapp, bindingName: "https");

identityApi
    .WithEnvironmentForServiceBinding("ApplyApiClient", applyApi, bindingName:"https")
    .WithEnvironmentForServiceBinding("WebAppClient", webapp, bindingName: "https");

builder.Build().Run();