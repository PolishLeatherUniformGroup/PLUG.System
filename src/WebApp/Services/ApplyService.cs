namespace PLUG.WebApp.Services;

public class ApplyService
{
    private readonly string remoteServiceBaseUrl = "/api/applications/";
    private readonly HttpClient _httpClient;

    public ApplyService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public Task CreateApplication(ApplicationForm applicationForm)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, this.remoteServiceBaseUrl);
        requestMessage.Content = JsonContent.Create(applicationForm);
        return this._httpClient.SendAsync(requestMessage);
    }
}

public record ApplicationForm()
{
    public IEnumerable<string> Recommendations { get; set; } = new List<string>();
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}