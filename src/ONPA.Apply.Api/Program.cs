
using System.Reflection;
using ONPA.Apply.Api.Application.Behavior;
using ONPA.Apply.Api.Application.IntegrationEvents;
using ONPA.Apply.Api.Extensions;
using ONPA.Apply.Infrastructure.DependencyInjection;
using ONPA.Common.Behaviors;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.AddApplicationServices();
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
