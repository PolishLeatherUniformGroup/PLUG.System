namespace PLUG.System.Apply.Api.Requests;

public record CreateApplicationRequest(string FirstName, string LastName, string Email, string Phone, string Address, string[] Recommenders)
{
}