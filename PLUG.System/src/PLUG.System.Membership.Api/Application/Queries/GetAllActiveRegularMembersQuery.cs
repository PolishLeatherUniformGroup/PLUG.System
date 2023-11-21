using PLUG.System.Common.Application;
using PLUG.System.Membership.Api.Application.Queries.Results;

namespace PLUG.System.Membership.Api.Application.Queries;

public record GetAllActiveRegularMembersQuery : ApplicationQueryBase<CollectionResult<MemberIdResult>>
{
    public int Page { get; init; } = 0;
    public int PageSize { get; init; } = 20;

    public GetAllActiveRegularMembersQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}