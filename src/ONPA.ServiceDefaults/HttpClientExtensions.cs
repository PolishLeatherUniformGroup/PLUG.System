using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ONPA.ServiceDefaults.ConfigurationModels;
using Polly;

namespace ONPA.ServiceDefaults;

[ExcludeFromCodeCoverage(Justification = "Tested via integration tests")]
public static class HttpClientExtensions
{
    public static IHttpClientBuilder AddAuthToken(this IHttpClientBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.TryAddTransient<HttpClientAuthorizationDelegatingHandler>();

        builder.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        return builder;
    }

    private class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (this._httpContextAccessor.HttpContext is HttpContext context)
            {
                var accessToken = await context.GetTokenAsync("access_token");

                if (accessToken is not null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }


    public static IHttpClientBuilder AddTransientWithHttpClient<TInterfaceType, TConcreteType>(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<HttpClient> configureClient = null)
            where TConcreteType : class, TInterfaceType
            where TInterfaceType : class
    {
        var httpClientConfiguration = new HttpClientConfiguration<TConcreteType>(configuration);

        return services
            .AddTransientWithHttpClient<TInterfaceType, TConcreteType>(httpClientConfiguration, configureClient);
    }

    public static IHttpClientBuilder AddTransientWithHttpClient<TInterfaceType, TConcreteType>(
         this IServiceCollection services,
         IConfiguration configuration,
         Func<HttpClient, IServiceProvider, TConcreteType> factory)
         where TConcreteType : class, TInterfaceType
         where TInterfaceType : class
    {
        var httpClientConfiguration = new HttpClientConfiguration<TConcreteType>(configuration);

        return services
            .AddTransientWithHttpClient<TInterfaceType, TConcreteType>(httpClientConfiguration, factory);
    }

    public static IHttpClientBuilder AddTransientWithHttpClient<TInterfaceType, TConcreteType>(
            this IServiceCollection services,
            HttpClientConfiguration<TConcreteType> httpClientConfiguration,
            Action<HttpClient> configureClient = null)
            where TConcreteType : class, TInterfaceType
            where TInterfaceType : class
    {

        return services
            .AddHttpClient<TInterfaceType, TConcreteType>(
                httpClientConfiguration.ClientName,
                client =>
                {
                    // Apply HttpClient configuration from the caller if it was provided.
                    // Call this first so we can override attempts to modify this library's configuration.
                    configureClient?.Invoke(client);

                    client.BaseAddress = httpClientConfiguration.Uri;
                })
            .AddRetryPolicy(httpClientConfiguration.RetryPolicyConfiguration);
           // .AddAuthToken();
    }

    public static IHttpClientBuilder AddTransientWithHttpClient<TInterfaceType, TConcreteType>(
            this IServiceCollection services,
            HttpClientConfiguration<TConcreteType> httpClientConfiguration,
            Func<HttpClient, IServiceProvider, TConcreteType> factory)
            where TConcreteType : class, TInterfaceType
            where TInterfaceType : class
    {
        return services
             .AddHttpClient<TInterfaceType, TConcreteType>(
                typeof(TConcreteType).Name,
                (client, provider) =>
                {
                    // Apply HttpClient configuration from the caller if it was provided.
                    // Call this first so we can override attempts to modify this library's configuration.
                    var instance = factory(client, provider);
                    client.BaseAddress = httpClientConfiguration.Uri;

                    return instance;
                })
            .AddRetryPolicy(httpClientConfiguration.RetryPolicyConfiguration);
           // .AddAuthToken();
    }

    private static IHttpClientBuilder AddRetryPolicy(this IHttpClientBuilder builder, RetryPolicyConfiguration retryConfiguration)
    {
        builder.AddTransientHttpErrorPolicy(policy =>
            retryConfiguration.WaitAndRetryTimeSpans.Any()
                ? policy.WaitAndRetryAsync(
                    retryConfiguration.WaitAndRetryTimeSpans)
                : policy.WaitAndRetryAsync(
                    retryConfiguration.MaximumRetryAttempts,
                    retryCount => TimeSpan.FromSeconds(retryCount)));

        return builder;
    }
}