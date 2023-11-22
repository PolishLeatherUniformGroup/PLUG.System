using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using PLUG.System.Apply.Api;

namespace PLUG.System.FunctionalTests.Apply;

public class ApplyApiFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly IHost _app;

    public IResourceBuilder<PostgresContainerResource> Postgres { get; private set; }
    public IResourceBuilder<RabbitMQContainerResource> Rabbit { get; private set; }

    public ApplyApiFixture()
    {
        var options = new DistributedApplicationOptions { AssemblyName = typeof(ApplyApiFixture).Assembly.FullName, DisableDashboard = true };
        var appBuilder = DistributedApplication.CreateBuilder(options);
        Postgres = appBuilder.AddPostgresContainer("ApplyDB");
        Rabbit = appBuilder.AddRabbitMQContainer("EventBus");
        _app = appBuilder.Build();
    }
    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
        await _app.StopAsync();
        if (_app is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync().ConfigureAwait(false);
        }
        else
        {
            _app.Dispose();
        }
    }

    public async Task InitializeAsync()
    {
        await _app.StartAsync();
    }
    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                { $"ConnectionStrings:{Postgres.Resource.Name}", Postgres.Resource.GetConnectionString() },
                { "EventBus:SubscriptionClientName", "Apply" },
            });
        });
        builder.ConfigureServices(services =>
        {
        });
        return base.CreateHost(builder);
    }
}