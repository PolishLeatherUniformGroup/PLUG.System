using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using ONPA.Apply.Api;

namespace ONPA.FunctionalTests.Apply;

public class ApplyTests: IClassFixture<ApplyApiFixture>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _httpClient;
    
    public ApplyTests(ApplyApiFixture fixture)
    {
        this._webApplicationFactory = fixture;
        this._httpClient = this._webApplicationFactory.CreateClient();
    }

  
}