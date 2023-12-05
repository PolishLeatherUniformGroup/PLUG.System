
using Asp.Versioning;
using ONPA.Apply.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.AddApplicationServices();
// builder.Services.AddApiVersioning(options =>
//     {
//         options.DefaultApiVersion = new ApiVersion(1, 0);
//         options.AssumeDefaultVersionWhenUnspecified = true;
//         options.ReportApiVersions = true;
//     })
//     .AddApiExplorer(options =>
//     {
//         options.GroupNameFormat = "'v'VVV";
//         options.SubstituteApiVersionInUrl = true;
//     });

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
