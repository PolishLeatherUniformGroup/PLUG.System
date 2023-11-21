namespace PLUG.System.Membership.Api.Application.Queries.Results;

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