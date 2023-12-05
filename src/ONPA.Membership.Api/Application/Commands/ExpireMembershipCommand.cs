using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record ExpireMembershipCommand :ApplicationCommandBase
{
    public Guid MemberId { get; init; }
    public DateTime ExpirationDate { get; init; }
    public string Reason { get; init; }

    public ExpireMembershipCommand(Guid memberId, DateTime expirationDate, string reason)
    {
        this.MemberId = memberId;
        this.ExpirationDate = expirationDate;
        this.Reason = reason;
    }
}