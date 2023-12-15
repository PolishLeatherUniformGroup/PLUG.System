namespace ONPA.Membership.Contract.Requests;

public record CreateMemberRequest(Guid TenantId, string FirstName, string LastName, string Email, string Phone, string Address, DateTime JoinDate, decimal PaidFeeAmount, string PaidFeeCurrency);