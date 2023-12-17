using AutoMapper;
using ONPA.Common.Application;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.QueryHandlers;

public sealed class GetMemberFeesQueryHandler: CollectionQueryHandlerBase<GetMemberFeesQuery, MemberFeeResult>
{
    private readonly IReadOnlyRepository<Infrastructure.ReadModel.MemberFee> _memberFeeRepository;
    private readonly IMapper _mapper;

    public GetMemberFeesQueryHandler(IReadOnlyRepository<Infrastructure.ReadModel.MemberFee> memberFeeRepository, IMapper mapper)
    {
        _memberFeeRepository = memberFeeRepository;
        _mapper = mapper;
    }

    public override async Task<CollectionResult<MemberFeeResult>> Handle(GetMemberFeesQuery request, CancellationToken cancellationToken)
    {
        var fees= await _memberFeeRepository.ManyByFilter(x => x.MemberId == request.MemberId, request.Page, request.Limit, cancellationToken: cancellationToken);
        return CollectionResult<MemberFeeResult>.FromValue(fees.Select(
            x => this._mapper.Map<MemberFeeResult>(x)), fees.Count());
    }
}