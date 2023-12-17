namespace ONPA.Membership.Contract.Responses;

public record MemberExpelResult(Guid MemberId,DateTime ExpelDate, string Justification, DateTime? AppealDate, string? AppealJustification, DateTime? AppealDecisionDate);