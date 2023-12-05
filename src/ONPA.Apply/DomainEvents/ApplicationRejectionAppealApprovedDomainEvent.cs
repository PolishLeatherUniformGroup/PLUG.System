using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationRejectionAppealApprovedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public DateTime ApplyDate { get; private set; }
    public DateTime ApproveDate { get; private set; }
    public Money PaidFee { get; private set; }

    public ApplicationRejectionAppealApprovedDomainEvent(string firstName, string lastName, string email,
        string phone,
        string address,
        DateTime applyDate,
        DateTime approveDate,
        Money paidFee)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Phone = phone;
        this.Address = address;
        this.ApplyDate = applyDate;
        this.ApproveDate = approveDate;
        this.PaidFee = paidFee;
    }

    private ApplicationRejectionAppealApprovedDomainEvent(Guid aggregateId, string firstName, string lastName,
        string email,
        string phone,
        string address,
        DateTime applyDate,
        DateTime approveDate,
        Money paidFee) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Phone = phone;
        this.Address = address;
        this.ApplyDate = applyDate;
        this.ApproveDate = approveDate;
        this.PaidFee = paidFee;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new ApplicationRejectionAppealApprovedDomainEvent(aggregateId,this.FirstName,this.LastName,this.Email,
            this.Phone,this.Address,this.ApplyDate,this.ApproveDate,this.PaidFee);
    }
}