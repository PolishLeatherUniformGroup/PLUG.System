using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.DomainEvents;
using PLUG.System.Membership.StateEvents;
using PLUG.System.SharedDomain;
using PLUG.System.SharedDomain.Helpers;

namespace PLUG.System.Membership.Domain;

public sealed partial class Member : AggregateRoot
{
    public CardNumber MemberNumber { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public DateTime JoinDate { get; private set; }
    public DateTime MembershipValidUntil { get; private set; }

    public bool IsValid => DateTime.UtcNow.Date < this.MembershipValidUntil.Date;
    
    private readonly IList<MembershipFee> _membershipFees = new List<MembershipFee>();
    public IEnumerable<MembershipFee> MembershipFees => this._membershipFees;
    public MembershipFee? CurrentFee => this._membershipFees.MaxBy(f => f.FeeEndDate);

    public bool IsFeeBalanced => this.CurrentFee is not null && this.CurrentFee.IsBalanced;
    
    public MembershipType MembershipType { get; private set; }

    public MembershipStatus Status { get; private set; }

    public DateTime? SuspendedDate { get; private set; }
    public DateTime? SuspendedUntil { get; private set; }
    public DateTime? ExpelDate { get; private set; }
    public DateTime? DeleteDate { get; private set; }
    public DateTime? AppealDeadline { get; private set; }
    public DateTime? AppealDate { get; private set; }
    public DateTime? AppealDecisionDate { get; private set; }
    public DateTime? LeaveDate { get;  private set; }
    public DateTime? ExpireDate { get; private set; }
    

    public Member(CardNumber cardNumber,string firstName, string lastName, string email, string phone,string address, DateTime joinDate,
        Money paidFee)
    {
        MemberNumber = cardNumber;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        JoinDate = joinDate;
        Status = MembershipStatus.Active;
        MembershipType = MembershipType.Regular;
        this.MembershipValidUntil = joinDate.ToYearEnd();
        
        var fee = new MembershipFee(paidFee, joinDate,joinDate.ToYearEnd(), paidFee,joinDate);
        this._membershipFees.Add(fee);

        var change = new MemberCreated(cardNumber, FirstName, LastName, Email, Phone, Address, JoinDate, fee);
        this.RaiseChangeEvent(change);

        var domainEvent = new MemberJoinedDomainEvent(this.MemberNumber, this.FirstName, this.Email);
        this.RaiseDomainEvent(domainEvent);
    }

    public void ModifyContactData(string email, string phone, string address)
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }
        this.Email = email;
        this.Phone = phone;
        this.Address = address;

        var change = new MemberContactDataModified(email, phone, address);
        this.RaiseChangeEvent(change);
    }

    public void MakeHonoraryMember()
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }
        this.MembershipType = MembershipType.Honorary;
        var change = new MemberTypeChanged(this.MembershipType);
        this.RaiseChangeEvent(change);
    }
    
    public void MakeRegularMember()
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }
        this.MembershipType = MembershipType.Regular;
        var change = new MemberTypeChanged(this.MembershipType);
        this.RaiseChangeEvent(change);
    }

    public void RequestFeePayment(Money feeAmount, DateTime dueDate, DateTime periodEnd)
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }
        if (_membershipFees.Any(f => f.FeeEndDate == periodEnd))
        {
            return;
        }
        var fee = new MembershipFee(feeAmount, dueDate, periodEnd);
        this._membershipFees.Add(fee);

        var change = new MemberFeeRequested(fee);
        this.RaiseChangeEvent(change);

        var domainEvent = new MemberFeePaymentRequestedDomainEvent(this.FirstName, this.Email,
            fee.DueAmount, fee.DueDate, fee.FeeEndDate);
        this.RaiseDomainEvent(domainEvent);
    }

    public void RegisterPaymentFee(Guid membershipFeeId, Money paidAmount, DateTime paidDate)
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }
        var fee = this._membershipFees.SingleOrDefault(f => f.Id == membershipFeeId);
        if (fee is null)
        {
            throw new EntityNotFoundException();
        }
        fee.AddPayment(paidAmount,paidDate);

        var change = new MemberFeePaid(membershipFeeId, paidAmount, paidDate);
        this.RaiseChangeEvent(change);
        if (!fee.DueAmount.IsZero())
        {
            var registerPaymentDomainEvent =
                new MemberFeePaymentRegisteredDomainEvent(FirstName, Email, paidAmount, fee.DueAmount, paidDate,fee.FeeEndDate);
            this.RaiseDomainEvent(registerPaymentDomainEvent);
        }
        if (fee.IsBalanced)
        {
            this.MembershipValidUntil = fee.FeeEndDate;

            var membershipExtended = new MembershipExtendedDomainEvent(FirstName, Email, MembershipValidUntil);
            this.RaiseDomainEvent(membershipExtended);
        }
    }

    public void SuspendMember(string justification, DateTime suspensionDate, DateTime suspendUntil, int daysToAppeal)
    {
        
    }

    public void AppealSuspension(string justification, DateTime receivedDate)
    {
        
    }

    public void AcceptAppealSuspension()
    {
        
    }

    public void DismissAppealSuspension()
    {
        
    }
   
    /// <summary>
    /// Member can be expelled for breaching rules.
    /// </summary>
    /// <param name="justification"></param>
    /// <param name="suspensionDate"></param>
    /// <param name="suspendUntil"></param>
    /// <param name="daysToAppeal"></param>
    public void ExpelMember(string justification, DateTime suspensionDate, DateTime suspendUntil, int daysToAppeal)
    {
        
    }
    
    public void AppealExpel(string justification, DateTime receivedDate)
    {
        
    }

    public void AcceptAppealExpel()
    {
        
    }

    public void DismissAppealExpel()
    {
        
    }

    /// <summary>
    /// Member can leave on free will.
    /// </summary>
    public void LeaveOrganization()
    {
        
    }

    /// <summary>
    /// Membershi expires, when fee is overdue, member dies, or lost rights to be member 
    /// </summary>
    public void MembershipExpired()
    {
        
    }
}