using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationFeePaymentRegistered : StateEventBase
{
    public Money PaidFee { get; private set; }
    public DateTime PaidDate { get; private set; }
   
    public ApplicationFeePaymentRegistered(Money paidFee, DateTime paidDate)
    {
        this.PaidFee = paidFee;
        this.PaidDate = paidDate;
    }

    private ApplicationFeePaymentRegistered(Guid tenantId, Guid aggregateId, long aggregateVersion, Money paidFee, DateTime paidDate) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.PaidFee = paidFee;
        this.PaidDate = paidDate;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationFeePaymentRegistered(tenantId, aggregateId, aggregateVersion, this.PaidFee,this.PaidDate);
    }
}