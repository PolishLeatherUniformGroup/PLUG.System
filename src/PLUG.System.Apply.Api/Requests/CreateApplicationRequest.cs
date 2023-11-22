namespace PLUG.System.Apply.Api.Requests;

public record CreateApplicationRequest(string FirstName, string LastName, string Email, string Address, string[] Recommenders)
{
}