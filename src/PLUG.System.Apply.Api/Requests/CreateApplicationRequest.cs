namespace PLUG.System.Apply.Api__OLD.Requests.Apply;

public record CreateApplicationRequest(string FirstName, string LastName, string Email, string Address, string[] Recommenders)
{
}