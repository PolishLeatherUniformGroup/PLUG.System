using AutoMapper;
using ONPA.Common.Application;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.QueryHandlers;

public sealed class GetMemberExpelsQueryHandler: CollectionQueryHandlerBase<GetMemberExpelsQuery, MemberExpelResult>
{
    private readonly IReadOnlyRepository<Infrastructure.ReadModel.MemberExpel> _memberExpelRepository;
    private readonly IMapper _mapper;
    public GetMemberExpelsQueryHandler(IReadOnlyRepository<Infrastructure.ReadModel.MemberExpel> memberExpelRepository, IMapper mapper)
    {
        _memberExpelRepository = memberExpelRepository;
        _mapper = mapper;
    }

    public override async Task<CollectionResult<MemberExpelResult>> Handle(GetMemberExpelsQuery request, CancellationToken cancellationToken)
    {
       var expels= await _memberExpelRepository.ManyByFilter(x => x.MemberId == request.MemberId, request.Page, request.Limit, cancellationToken: cancellationToken);
       return CollectionResult<MemberExpelResult>.FromValue(
           expels.Select(x => _mapper.Map<MemberExpelResult>(x)), expels.Count());
    }
}