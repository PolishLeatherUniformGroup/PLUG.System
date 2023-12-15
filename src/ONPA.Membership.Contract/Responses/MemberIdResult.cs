namespace ONPA.Membership.Contract.Responses;

public record MemberIdResult
{
    public Guid Id { get; init; }

    public MemberIdResult(Guid id)
    {
        this.Id = id;
    }
}