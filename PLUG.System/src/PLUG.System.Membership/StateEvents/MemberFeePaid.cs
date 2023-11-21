using PLUG.System.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Membership.StateEvents;

public sealed class MemberFeePaid : StateEventBase
{
    public Guid FeeId { get; private set; }
    public Money PaidAmount { get; private set; }
    public DateTime PaidDate { get; private set; }

    public MemberFeePaid(Guid feeId, Money paidAmount, DateTime paidDate)
    {
        FeeId = feeId;
        PaidAmount = paidAmount;
        PaidDate = paidDate;
    }

    private MemberFeePaid(Guid aggregateId, long aggregateVersion, Guid feeId, Money paidAmount, DateTime paidDate) : base(aggregateId, aggregateVersion)
    {
        FeeId = feeId;
        PaidAmount = paidAmount;
        PaidDate = paidDate;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberFeePaid(aggregateId, aggregateVersion, FeeId, PaidAmount, PaidDate);
    }
}