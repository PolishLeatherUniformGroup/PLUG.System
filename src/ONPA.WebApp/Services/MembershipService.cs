namespace ONPA.WebApp.Services;

public class MembershipService
{
    private readonly HttpClient httpClient;

    public MembershipService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
}