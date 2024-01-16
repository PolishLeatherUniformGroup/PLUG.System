using Microsoft.FluentUI.AspNetCore.Components;
using ONPA.WebApp.Components;
using ONPA.WebApp.Services;
using ONPA.WebApp.Services.Abstractions;
using System.Net.Http.Headers;
using System.Net.Mime;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authentication;
using ONPA.ServiceDefaults;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();
builder.Services.AddLocalization(Options =>
    Options.ResourcesPath = "Resources");
builder.Services.AddControllers();
builder.Services.AddBff();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "cookie";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("cookie", options =>
    {
        options.Cookie.Name = "onpa";
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = configuration["IdentityServer:Authority"];
        options.ClientId = configuration["IdentityServer:ClientId"];
        options.ClientSecret = configuration["IdentityServer:ClientSecret"];
        options.ResponseType = "code";
        options.ResponseMode = "query";
        options.MapInboundClaims = false;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("apply-api");
        options.Scope.Add("gatherings-api");
        options.Scope.Add("membership-api");
        options.Scope.Add("organizations-api");
        options.Scope.Add("offline_access");
    });

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
builder.Services.AddTransientWithHttpClient<IOrganizationService, OrganizationService>(configuration, client =>
{
    client.BaseAddress = new Uri(configuration["Services:OrganizationService:Uri"]);
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

app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.UseRequestLocalization(localizationOptions);

app.MapBffManagementEndpoints();

app.MapRazorComponents<App>()
.AddInteractiveServerRenderMode();

app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();
 //app.UseMultiTenant();
app.Run();
