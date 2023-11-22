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
        this.FeeId = feeId;
        this.PaidAmount = paidAmount;
        this.PaidDate = paidDate;
    }

    private MemberFeePaid(Guid aggregateId, long aggregateVersion, Guid feeId, Money paidAmount, DateTime paidDate) : base(aggregateId, aggregateVersion)
    {
        this.FeeId = feeId;
        this.PaidAmount = paidAmount;
        this.PaidDate = paidDate;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberFeePaid(aggregateId, aggregateVersion, this.FeeId, this.PaidAmount, this.PaidDate);
    }
}