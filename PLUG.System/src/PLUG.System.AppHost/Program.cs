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

 var identityApi = builder.AddProject<Projects.PLUG_System_Identity_Api>("plug-identity-api")
     .WithReference(identityDb);

 var applyApi = builder.AddProject<Projects.PLUG_System_Apply_Api>("plug-apply-api")
    .WithReference(applyDb)
    .WithReference(rabbitMq)
    
    .WithEnvironmentForServiceBinding("Identity__Url", identityApi);
var membershipApi = builder.AddProject<Projects.PLUG_System_Membership_Api>("plug-membership-api");
builder.Build().Run();