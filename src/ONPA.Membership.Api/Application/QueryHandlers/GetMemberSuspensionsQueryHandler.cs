using AutoMapper;
using ONPA.Common.Application;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.QueryHandlers;

public sealed class GetMemberSuspensionsQueryHandler: CollectionQueryHandlerBase<GetMemberSuspensionsQuery, MemberSuspensionResult>
{
    private readonly IReadOnlyRepository<Infrastructure.ReadModel.MemberSuspension> _memberSuspensionRepository;
    private readonly IMapper _mapper;
    public GetMemberSuspensionsQueryHandler(IReadOnlyRepository<Infrastructure.ReadModel.MemberSuspension> memberSuspensionRepository, IMapper mapper)
    {
        _memberSuspensionRepository = memberSuspensionRepository;
        _mapper = mapper;
    }

    public override async Task<CollectionResult<MemberSuspensionResult>> Handle(GetMemberSuspensionsQuery request, CancellationToken cancellationToken)
    {
        var suspensions= await _memberSuspensionRepository.ManyByFilter(x => x.MemberId == request.MemberId, request.Page, request.Limit, cancellationToken: cancellationToken);
        return CollectionResult<MemberSuspensionResult>.FromValue(
            suspensions.Select(x => _mapper.Map<MemberSuspensionResult>(x)), suspensions.Count());
    }
}