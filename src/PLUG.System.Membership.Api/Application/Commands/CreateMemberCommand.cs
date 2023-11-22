using PLUG.System.Common.Application;
using PLUG.System.SharedDomain;

namespace PLUG.System.Membership.Api.Application.Commands;

public sealed record CreateMemberCommand : ApplicationCommandBase
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public string Address { get; init; }
    public DateTime JoinDate { get; init; }
    public Money PaidFee { get; init; }

    public CreateMemberCommand(string firstName, string lastName, string email, string phone, string address, DateTime joinDate, Money paidFee)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        JoinDate = joinDate;
        PaidFee = paidFee;
    }
}