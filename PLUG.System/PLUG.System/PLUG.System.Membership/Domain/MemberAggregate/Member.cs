using PLUG.System.Common.Domain;
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
    
    public bool IsFeeBalanced => this.CurrentFee is null || this.CurrentFee.PaidAmount >= this.CurrentFee?.DueAmount;
    
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
}