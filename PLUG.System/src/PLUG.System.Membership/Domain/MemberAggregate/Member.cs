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
    private MembershipSuspension? _suspension;
    private MembershipExpel? _expel;
    public IEnumerable<MembershipFee> MembershipFees => this._membershipFees;
    public MembershipFee? CurrentFee => this._membershipFees.MaxBy(f => f.FeeEndDate);

    public bool IsFeeBalanced => this.CurrentFee is not null && this.CurrentFee.IsBalanced;
    
    public MembershipType MembershipType { get; private set; }

    public MembershipStatus Status { get; private set; }

    public MembershipSuspension? Suspension => this._suspension;
    public MembershipExpel? Expel => this._expel;


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
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }

        if (this._suspension is not null)
        {
            throw new AggregateInvalidStateException();
        }

        this._suspension = new MembershipSuspension(suspensionDate, suspendUntil, justification, suspensionDate.AddDays(daysToAppeal));
 this.Status = MembershipStatus.Suspended;
        
        var changeEvent = new MemberSuspended(this._suspension);
        this.RaiseChangeEvent(changeEvent);
        
        var domainEvent = new MemberSuspendedDomainEvent(this.FirstName, this.Email, this._suspension.SuspensionJustification, 
            this._suspension.SuspensionDate, this._suspension.SuspendedUntil, this._suspension.AppealDeadline);
        this.RaiseDomainEvent(domainEvent);
    }

    public void AppealSuspension(string justification, DateTime receivedDate)
    {
        if (this.Status != MembershipStatus.Suspended)
        {
            throw new AggregateInvalidStateException();
        }

        if (this._suspension is null)
        {
            throw new AggregateInvalidStateException();
        }

        this._suspension.Appeal(receivedDate,justification);

        var change = new MemberSuspensionAppealReceived(justification, receivedDate);
        this.RaiseChangeEvent(change);

        if (receivedDate.Date > this._suspension.AppealDeadline.Date)
        {
            this._suspension.RejectAppeal(receivedDate, "Odwołanie wpłynęło po terminie.");
      
            var autoDecision = new SuspensionAppealDismissed(this._suspension.AppealDecisionDate.GetValueOrDefault(), this._suspension.AppealDecisionJustification!);
            this.RaiseChangeEvent(autoDecision);

            var rejectionEvent = new MemberSuspensionAppealDismissedDomainEvent(this.FirstName,this.Email, this._suspension!.AppealDecisionDate.GetValueOrDefault(), this._suspension.AppealDecisionJustification);
            this.RaiseDomainEvent(rejectionEvent);
            return;
        }

        var domainEvent = new MemberSuspensionAppealReceivedDomainEvent(this.FirstName,this.Email,this._suspension!.AppealDate.GetValueOrDefault());
        this.RaiseDomainEvent(domainEvent);
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
    /// <param name="expelDate"></param>
    /// <param name="daysToAppeal"></param>
    public void ExpelMember(string justification, DateTime expelDate,  int daysToAppeal)
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }

        if (this._expel is not null)
        {
            throw new AggregateInvalidStateException();
        }

        this._expel = new MembershipExpel(expelDate,  justification, expelDate.AddDays(daysToAppeal));
        this.Status = MembershipStatus.Expelled;
        
        var changeEvent = new MemberExpelled(this._expel);
        this.RaiseChangeEvent(changeEvent);
        
        var domainEvent = new MemberExpelledDomainEvent(this.FirstName, this.Email, this._expel.ExpelJustification, 
            this._expel.ExpelDate,  this._expel.AppealDeadline);
        this.RaiseDomainEvent(domainEvent);
    }
    
    public void AppealExpel(string justification, DateTime receivedDate)
    {
        if (this.Status != MembershipStatus.Suspended)
        {
            throw new AggregateInvalidStateException();
        }

        if (this._expel is null)
        {
            throw new AggregateInvalidStateException();
        }

        this._expel.Appeal(receivedDate,justification);

        var change = new MemberExpelAppealReceived(justification, receivedDate);
        this.RaiseChangeEvent(change);

        if (receivedDate.Date > this._expel.AppealDeadline.Date)
        {
            this._expel.RejectAppeal(receivedDate, "Odwołanie wpłynęło po terminie.");
      
            var autoDecision = new ExpelAppealDismissed(this._expel.AppealDecisionDate.GetValueOrDefault(), this._expel.AppealDecisionJustification!);
            this.RaiseChangeEvent(autoDecision);

            var rejectionEvent = new MemberExpelAppealDismissedDomainEvent(this.FirstName,this.Email, this._suspension!.AppealDecisionDate.GetValueOrDefault(), this._suspension.AppealDecisionJustification);
            this.RaiseDomainEvent(rejectionEvent);
            return;
        }

        var domainEvent = new MemberExpelAppealReceivedDomainEvent(this.FirstName,this.Email,this._suspension!.AppealDate.GetValueOrDefault());
        this.RaiseDomainEvent(domainEvent);
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