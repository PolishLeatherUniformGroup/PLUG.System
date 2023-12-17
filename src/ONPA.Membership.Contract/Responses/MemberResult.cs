namespace ONPA.Membership.Contract.Responses;

public record MemberResult(Guid MemberId, string CardNumber, string FirstName, string LastName, string Email, int Status, DateTime JoinDate);