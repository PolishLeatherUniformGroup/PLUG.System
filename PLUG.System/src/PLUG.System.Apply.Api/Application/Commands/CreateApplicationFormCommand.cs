using PLUG.System.Common.Application;

namespace PLUG.System.Apply.Api.Application.Commands;

public sealed record CreateApplicationFormCommand(string FirstName, string LastName, string Email, string Phone,
        List<string> Recommendations,
        string Address)
    : ApplicationCommandBase
{
    public string FirstName { get; private set; } = FirstName ?? throw new ArgumentNullException(nameof(FirstName));
    public string LastName { get; private set; } = LastName ?? throw new ArgumentNullException(nameof(LastName));
    public string Email { get; private set; } = Email ?? throw new ArgumentNullException(nameof(Email));
    public string Phone { get; private set; } = Phone ?? throw new ArgumentNullException(nameof(Phone));
    public List<string> Recommendations { get; private set; } = Recommendations ?? throw new ArgumentNullException(nameof(Recommendations));
    public string Address { get; private set; } = Address ?? throw new ArgumentNullException(nameof(Address));
}