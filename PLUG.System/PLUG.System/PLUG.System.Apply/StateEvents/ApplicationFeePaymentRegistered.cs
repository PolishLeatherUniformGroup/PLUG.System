using PLUG.System.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationFeePaymentRegistered : StateEventBase
{
    public Money PaidFee { get; private set; }
    public DateTime PaidDate { get; private set; }
    public DateTime ExpectedDecisionDate { get; private set; }

    public ApplicationFeePaymentRegistered(Money paidFee, DateTime paidDate, DateTime expectedDecisionDate)
    {
        this.PaidFee = paidFee;
        this.PaidDate = paidDate;
        this.ExpectedDecisionDate = expectedDecisionDate;
    }

    private ApplicationFeePaymentRegistered(Guid aggregateId, long aggregateVersion, Money paidFee, DateTime paidDate, DateTime expectedDecisionDate) : base(aggregateId, aggregateVersion)
    {
        this.PaidFee = paidFee;
        this.PaidDate = paidDate;
        this.ExpectedDecisionDate = expectedDecisionDate;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationFeePaymentRegistered(aggregateId, aggregateVersion, this.PaidFee,this.PaidDate,this.ExpectedDecisionDate);
    }
}