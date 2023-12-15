using ONPA.Common.Application;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.QueryHandlers;

public class GetMembersByStatusQueryHandler: CollectionQueryHandlerBase<GetMembersByStatusQuery, MemberResult>
{
    
    public override Task<CollectionResult<MemberResult>> Handle(GetMembersByStatusQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}