namespace ONPA.Membership.Api.Application.Queries.Results;

public record MemberIdResult
{
    public Guid Id { get; init; }

    public MemberIdResult(Guid id)
    {
        Id = id;
    }
}