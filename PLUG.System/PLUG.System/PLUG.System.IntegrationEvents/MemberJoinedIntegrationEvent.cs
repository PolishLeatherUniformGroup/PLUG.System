using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public record MemberJoinedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public string Address { get; init; }
    public DateTime JoinDate { get; init; }
    public decimal PaidFeeAmount { get; init; }
    public string PaidFeeCurrency { get; init; }

    public MemberJoinedIntegrationEvent(string firstName, string lastName, string email,
        string phone,string address,
        DateTime joinDate,
        decimal paidFeeAmount,
        string paidFeeCurrency)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Phone = phone;
        this.Address = address;
        this.JoinDate = joinDate;
        this.PaidFeeAmount = paidFeeAmount;
        this.PaidFeeCurrency = paidFeeCurrency;
    }
}