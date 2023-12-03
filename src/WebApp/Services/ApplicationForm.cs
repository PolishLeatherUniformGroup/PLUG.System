namespace PLUG.WebApp.Services;

public record ApplicationForm()
{
    public IEnumerable<string> Recommendations { get; set; } = new List<string>();
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}