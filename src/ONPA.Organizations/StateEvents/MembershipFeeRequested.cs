using ONPA.Common.Domain;
using ONPA.Organizations.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Organizations.StateEvents;

public class MembershipFeeRequested : StateEventBase
{
    public Guid Id { get; set; }
    public Money Amount { get; private set; }
    public int Year { get; private set; }

    public MembershipFeeRequested(Guid id, Money amount, int year)
    {
        this.Amount = amount;
        this.Year = year;
        this.Id = id;
    }

    private MembershipFeeRequested(Guid aggregateId, long aggregateVersion, Guid id, Money amount, int year) : base(aggregateId, aggregateVersion)
    {
        this.Amount = amount;
        this.Year = year;
        this.Id = id;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MembershipFeeRequested(aggregateId, aggregateVersion,this.Id, this.Amount, this.Year);
    }
}