using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Membership.StateEvents;

public class MemberCreated : StateEventBase
{
    public CardNumber CardNumber { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public DateTime JoinDate { get; private set; }
    public MembershipFee PaidFee { get; private set; }

    public MemberCreated(CardNumber cardNumber, string firstName, string lastName, string email, string phone, string address, DateTime joinDate, MembershipFee paidFee)
    {
        CardNumber = cardNumber;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        JoinDate = joinDate;
        PaidFee = paidFee;
    }

    private MemberCreated(Guid aggregateId, long aggregateVersion, CardNumber cardNumber, string firstName, string lastName, string email, string phone, string address, DateTime joinDate, MembershipFee paidFee) : base(aggregateId, aggregateVersion)
    {
        CardNumber = cardNumber;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        JoinDate = joinDate;
        PaidFee = paidFee;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberCreated(aggregateId, aggregateVersion, this.CardNumber, this.FirstName, this.LastName,
            this.Email, this.Phone, this.Address, this.JoinDate, this.PaidFee);
    }
}