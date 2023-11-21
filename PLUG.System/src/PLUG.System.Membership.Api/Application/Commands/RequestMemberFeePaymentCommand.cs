using PLUG.System.Common.Application;
using PLUG.System.SharedDomain;

namespace PLUG.System.Membership.Api.Application.Commands;

public sealed record RequestMemberFeePaymentCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }
    public Money FeeAmount { get; init; }
    public DateTime DueDate { get; init; }
    public DateTime EndOfPeriod { get; init; }

    public RequestMemberFeePaymentCommand(Guid memberId, Money feeAmount, DateTime dueDate, DateTime endOfPeriod)
    {
        MemberId = memberId;
        FeeAmount = feeAmount;
        DueDate = dueDate;
        EndOfPeriod = endOfPeriod;
    }
}