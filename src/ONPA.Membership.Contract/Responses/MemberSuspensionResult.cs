namespace ONPA.Membership.Contract.Responses;

public record MemberSuspensionResult(
    Guid MemberId,
    DateTime SuspensionDate,
    DateTime SuspendedUntil,
    string Justification,
    DateTime? AppealDate,
    string? AppealJustification,
    DateTime? AppealDecisionDate);
