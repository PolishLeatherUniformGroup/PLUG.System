using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record CreateApplicationFormCommand
    : ApplicationCommandBase
{
    
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public List<string> Recommendations { get; init; } 
    public string Address { get; init; }

    public CreateApplicationFormCommand(string firstName, string lastName, string email,
        string phone,
        List<string> recommendations,
        string address)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Phone = phone;
        this.Recommendations = recommendations;
        this.Address = address;
    }
}