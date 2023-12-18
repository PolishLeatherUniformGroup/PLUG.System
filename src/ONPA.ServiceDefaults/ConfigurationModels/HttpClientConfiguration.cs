using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONPA.ServiceDefaults.ConfigurationModels
{
    public class HttpClientConfiguration : IHttpClientConfiguration
    {
        private readonly List<Exception> exceptions = new List<Exception>();
        private string clientName;
        private Uri uri;

        public HttpClientConfiguration(string clientTypeName, IConfiguration configuration)
        {
            this.ClientName = clientTypeName;

            var clientSection = configuration.GetSection($"Services:{clientTypeName}");

            if (!clientSection.Exists())
            {
                throw new ArgumentNullException(clientTypeName, $"No child property of 'Services' in the configuration matched the parameter provided.");
            }

            this.Uri = clientSection.GetValue<Uri>("Uri");

            if (this.Uri != null && !this.Uri.AbsoluteUri.EndsWith("/"))
            {
                this.Uri = new Uri($"{this.Uri.AbsoluteUri}/");
            }

            var retrySection = clientSection.GetSection("Retry");

            this.RetryPolicyConfiguration = new RetryPolicyConfiguration(retrySection);
           
            this.EnsureConfigurationIsValid();
        }

        public HttpClientConfiguration(
            string clientName,
            Uri uri,
            RetryPolicyConfiguration retryPolicyConfiguration = null)
        {
            this.clientName = clientName;
            this.Uri = uri;
            this.RetryPolicyConfiguration = retryPolicyConfiguration ?? new RetryPolicyConfiguration();

            this.EnsureConfigurationIsValid();
        }

        public Uri Uri
        {
            get => this.uri;

            private set
            {
                if (value is null)
                {
                    this.exceptions.Add(new ArgumentNullException(nameof(this.Uri)));
                }
                else if (!value.IsAbsoluteUri)
                {
                    this.exceptions.Add(new ArgumentException($"Invalid BaseUri value for '{this.clientName}'"));
                }

                this.uri = value;
            }
        }

        public string ClientName
        {
            get => this.clientName;

            private set
            {
                if (value is null)
                {
                    this.exceptions.Add(new ArgumentNullException(nameof(this.ClientName)));
                }

                this.clientName = value;
            }
        }

        public RetryPolicyConfiguration RetryPolicyConfiguration { get; }

        private void EnsureConfigurationIsValid()
        {
            switch (this.exceptions.Count)
            {
                case 0:
                    return;
                case 1:
                    throw this.exceptions[0];
                default:
                    throw new AggregateException(this.exceptions);
            }
        }

        private Func<string, string> SetupOktaOverride(IConfiguration rootOkta, IConfiguration serviceOkta)
        {
            return key =>
            {
                if (serviceOkta is object)
                {
                    var value = serviceOkta.GetValue<string>(key);

                    if (value is object)
                    {
                        return value;
                    }
                }

                if (rootOkta is object)
                {
                    var value = rootOkta.GetValue<string>(key);

                    if (value is object)
                    {
                        return value;
                    }
                }

                return default;
            };
        }
    }


    public sealed class HttpClientConfiguration<TClientType> : HttpClientConfiguration
    {
        /// <summary>
        /// Construct a new <see cref="HttpClientConfiguration{TClientType}"/>.
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <exception cref="ArgumentNullException">Missing a required field.</exception>
        /// <exception cref="AggregateException">Multiple validation errors are thrown together.</exception>
        public HttpClientConfiguration(IConfiguration configuration)
            : base(typeof(TClientType).Name, configuration)
        {
        }

        /// <summary>
        /// Construct a new <see cref="HttpClientConfiguration{TClientType}"/>.
        /// </summary>
        /// <param name="uri">The base uri of the external service this client will call.</param>
        /// <param name="oktaClientCredentialsConfiguration">Configuration for the auth integration.</param>
        /// <param name="retryPolicyConfiguration">Optional retry policy configuration.</param>
        /// <exception cref="ArgumentNullException">Missing a required field.</exception>
        /// <exception cref="AggregateException">Multiple validation errors are thrown together.</exception>
        public HttpClientConfiguration(
            Uri uri,      
            RetryPolicyConfiguration retryPolicyConfiguration = null)
            : base(typeof(TClientType).Name, uri, retryPolicyConfiguration)
        {
        }
    }
}
