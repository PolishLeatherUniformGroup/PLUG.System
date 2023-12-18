namespace ONPA.ServiceDefaults.ConfigurationModels
{
    public interface IHttpClientConfiguration
    {
        string ClientName { get; }
  
        RetryPolicyConfiguration RetryPolicyConfiguration { get; }
    }
}
