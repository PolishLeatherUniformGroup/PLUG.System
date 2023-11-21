using PLUG.System.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Membership.DomainEvents;

public sealed class MemberFeePaymentRequestedDomainEvent :DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public Money RequestedFee { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime Period { get; private set; }

    public MemberFeePaymentRequestedDomainEvent(string firstName, string email, Money requestedFee, DateTime dueDate, DateTime period)
    {
        FirstName = firstName;
        Email = email;
        RequestedFee = requestedFee;
        DueDate = dueDate;
        Period = period;
    }

    private MemberFeePaymentRequestedDomainEvent(Guid aggregateId, string firstName, string email, Money requestedFee, DateTime dueDate, DateTime period) : base(aggregateId)
    {
        FirstName = firstName;
        Email = email;
        RequestedFee = requestedFee;
        DueDate = dueDate;
        Period = period;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberFeePaymentRequestedDomainEvent(aggregateId, FirstName, Email, RequestedFee, DueDate, Period);
    }
}