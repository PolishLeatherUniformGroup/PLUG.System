using PLUG.System.Common.Application;
using PLUG.System.SharedDomain;

namespace PLUG.System.Membership.Api.Application.Commands;

public sealed record RegisterMemberFeePaymentCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }
    public Guid FeeId { get; init; }
    public Money FeeAmount { get; init; }
    public DateTime PaidDate { get; init; }

    public RegisterMemberFeePaymentCommand(Guid memberId, Guid feeId, Money feeAmount, DateTime paidDate)
    {
        this.MemberId = memberId;
        this.FeeAmount = feeAmount;
        this.PaidDate = paidDate;
        this.FeeId = feeId;
    }
}