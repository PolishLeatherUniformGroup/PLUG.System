namespace ONPA.Membership.Contract.Responses;

public record MemberValidationResult
{
    public Guid MemberId { get;  init; }
    public string MemberNumber { get;  init; }

    public MemberValidationResult(Guid memberId, string memberNumber)
    {
        this.MemberId = memberId;
        this.MemberNumber = memberNumber;
    }
}