using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using PLUG.System.Apply.Api;
using PLUG.System.Apply.Api.Requests;

namespace PLUG.System.FunctionalTests.Apply;

public class ApplyTests: IClassFixture<ApplyApiFixture>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _httpClient;
    
    public ApplyTests(ApplyApiFixture fixture)
    {
        _webApplicationFactory = fixture;
        _httpClient = _webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task CreateApplication()
    {
        var createApplicationRequest = new CreateApplicationRequest("Frst", "Last","first.last@somewhere.com", "Address", new[] { "PLUG-0001" });
        var content = new StringContent(JsonSerializer.Serialize(createApplicationRequest));
        var response = await this._httpClient.PostAsync(new Uri("api/applications"), content);
        response.EnsureSuccessStatusCode();
        
    }
}