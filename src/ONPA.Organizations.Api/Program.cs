using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Behaviors;
using ONPA.Common.Infrastructure;
using ONPA.Organizations.Api.Application.Behaviors;
using ONPA.Organizations.Api.Application.IntegrationEvents;
using ONPA.Organizations.Infrastructure.Database;
using ONPA.Organizations.Infrastructure.DataSeed;
using ONPA.Organizations.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMediatR(configuration=>
{
    configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    configuration.AddOpenBehavior(typeof(CommandLoggingBehavior<,>));
    configuration.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
    configuration.AddOpenBehavior(typeof(TransactionalBehavior<,>));
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<IIntegrationEventService, IntegrationEventService>();
builder.AddRabbitMqEventBus("EventBus");

builder.AddNpgsqlDbContext<OrganizationsContext>("onpa_db");

builder.Services.AddMigration<OrganizationsContext, OrganizationSeed>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();