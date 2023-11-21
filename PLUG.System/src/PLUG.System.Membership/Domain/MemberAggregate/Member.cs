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
        this.Email = email;
        this.Phone = phone;
        this.Address = address;

        var change = new MemberContactDataModified(email, phone, address);
        this.RaiseChangeEvent(change);
    }

    public void MakeHonoraryMember()
    {
        this.MembershipType = MembershipType.Honorary;
        var change = new MemberTypeChanged(this.MembershipType);
        this.RaiseChangeEvent(change);
    }
    
    public void MakeRegularMember()
    {
        this.MembershipType = MembershipType.Regular;
        var change = new MemberTypeChanged(this.MembershipType);
        this.RaiseChangeEvent(change);
    }

    public void RequestFeePayment(Money feeAmount, DateTime dueDate, DateTime periodEnd)
    {
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
}