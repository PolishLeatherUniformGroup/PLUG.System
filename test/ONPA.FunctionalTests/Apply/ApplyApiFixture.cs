﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using ONPA.Apply.Api;

namespace ONPA.FunctionalTests.Apply;

public class ApplyApiFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly IHost _app;

    public IResourceBuilder<PostgresContainerResource> Postgres { get; private set; }
    public IResourceBuilder<RabbitMQContainerResource> Rabbit { get; private set; }

    public ApplyApiFixture()
    {
        var options = new DistributedApplicationOptions { AssemblyName = typeof(ApplyApiFixture).Assembly.FullName, DisableDashboard = true };
        var appBuilder = DistributedApplication.CreateBuilder(options);
        this.Postgres = appBuilder.AddPostgresContainer("ApplyDB");
        this.Rabbit = appBuilder.AddRabbitMQContainer("EventBus");
        this._app = appBuilder.Build();
    }
    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
        await this._app.StopAsync();
        if (this._app is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync().ConfigureAwait(false);
        }
        else
        {
            this._app.Dispose();
        }
    }

    public async Task InitializeAsync()
    {
        await this._app.StartAsync();
    }
    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                { $"ConnectionStrings:{this.Postgres.Resource.Name}", this.Postgres.Resource.GetConnectionString() },
                { "EventBus:SubscriptionClientName", "Apply" },
            });
        });
        builder.ConfigureServices(services =>
        {
        });
        return base.CreateHost(builder);
    }
}