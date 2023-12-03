using System.Net;
using PLUG.WebApp.Results;
using Polly;

namespace PLUG.WebApp.Services;

public class ApplyService
{
    private readonly string remoteServiceBaseUrl = "/api/applications/";
    private readonly HttpClient _httpClient;

    public ApplyService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> CreateApplication(ApplicationForm applicationForm)
    {
        
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, this.remoteServiceBaseUrl);
            requestMessage.Content = JsonContent.Create(applicationForm);
            var response = await this._httpClient.SendAsync(requestMessage);
 
            return response;
    }
}