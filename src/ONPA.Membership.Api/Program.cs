using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Behaviors;
using ONPA.Common.Infrastructure;
using ONPA.Membership.Api.Application.Behaviors;
using ONPA.Membership.Api.Application.IntegrationEvents;
using ONPA.Membership.Infrastructure.Database;
using ONPA.Membership.Infrastructure.DataSeed;
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
#if LOCAL
builder.AddNpgsqlDbContext<MembershipContext>("onpa_db", configureDbContextOptions: dbContextOptionsBuilder =>
{
    dbContextOptionsBuilder.UseNpgsql(builder =>
    {
        builder.UseVector();
    });
});
#else
builder.AddNpgsqlDbContext<MembershipContext>("onpa_db");
#endif
builder.Services.AddMigration<MembershipContext, MembershipSeed>();

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