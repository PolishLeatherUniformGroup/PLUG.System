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

  
}