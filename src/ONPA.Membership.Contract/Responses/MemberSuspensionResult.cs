namespace ONPA.Membership.Contract.Responses;

public record MemberSuspensionResult
{
    public DateTime SuspensionDate { get; init; }
    public DateTime SuspendedUntil { get; init; }
    public string Justification { get; init; }
    public DateTime? AppealDate { get; init; }
    public string? AppealJustification { get; init; }
    public DateTime? AppealDecisionDate { get; init; }
    public string? AppealDecisionJustification { get; init; }
}

public record MemberExpelResult
{
    public DateTime ExpelDate { get; init; }
    public string Justification { get; init; }
    public DateTime? AppealDate { get; init; }
    public string? AppealJustification { get; init; }
    public DateTime? AppealDecisionDate { get; init; }
    public string? AppealDecisionJustification { get; init; }
}