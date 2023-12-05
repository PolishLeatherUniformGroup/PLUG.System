using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public record ModifyMemberContactDataCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public string Address { get; init; }

    public ModifyMemberContactDataCommand(Guid memberId, string email, string phone, string address)
    {
        MemberId = memberId;
        Email = email;
        Phone = phone;
        Address = address;
    }
}