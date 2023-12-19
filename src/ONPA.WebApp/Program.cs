using Microsoft.FluentUI.AspNetCore.Components;
using ONPA.WebApp.Components;
using ONPA.WebApp.Services;
using ONPA.WebApp.Services.Abstractions;
using System.Net.Http.Headers;
using System.Net.Mime;
using Finbuckle.MultiTenant;
using ONPA.ServiceDefaults;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
var configuration = builder.Configuration;
builder.Services.AddMultiTenant<TenantInfo>(x =>
    {
        x.IgnoredIdentifiers.Add("wwwroot");
        x.IgnoredIdentifiers.Add("css");
        x.IgnoredIdentifiers.Add("js");
        x.IgnoredIdentifiers.Add("lib");
        x.IgnoredIdentifiers.Add("images");
        x.IgnoredIdentifiers.Add("fonts");
        x.IgnoredIdentifiers.Add("_framework");
        x.IgnoredIdentifiers.Add("_blazor");
    })
    .WithHttpRemoteStore($"{configuration["Services:OrganizationService:Uri"]}/api/tenants")
    .WithBasePathStrategy(options =>
    {
        options.RebaseAspNetCorePathBase = true;
    });
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();
builder.Services.AddLocalization(Options =>
    Options.ResourcesPath = "Resources");
builder.Services.AddControllers();


builder.Services.AddTransientWithHttpClient<IApplyService, ApplyService>(configuration, client =>
{
    client.BaseAddress = new Uri(configuration["Services:ApplyService:Uri"]);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
});
builder.Services.AddTransientWithHttpClient<IMembershipService, MembershipService>(configuration, client =>
{
    client.BaseAddress = new Uri(configuration["Services:MembershipService:Uri"]);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
});


var app = builder.Build();


app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseAntiforgery();

var supportedCultures = new[] { "pl-PL", "en-US" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.MapRazorComponents<App>()
.AddInteractiveServerRenderMode();

app.MapControllers();
app.UseMultiTenant();
app.Run();
