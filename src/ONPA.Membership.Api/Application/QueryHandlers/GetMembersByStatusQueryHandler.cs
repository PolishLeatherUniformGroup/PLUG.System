using AutoMapper;
using ONPA.Common.Application;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Responses;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Api.Application.QueryHandlers;

public class GetMembersByStatusQueryHandler: CollectionQueryHandlerBase<GetMembersByStatusQuery, MemberResult>
{
    private readonly IReadOnlyRepository<Member> _memberRepository;
    private readonly IMapper _mapper;
    
    public GetMembersByStatusQueryHandler(IReadOnlyRepository<Member> memberRepository, IMapper mapper)
    {
        this._memberRepository = memberRepository;
        this._mapper = mapper;
    }
    
    
    public override async Task<CollectionResult<MemberResult>> Handle(GetMembersByStatusQuery request, CancellationToken cancellationToken)
    {
       var members = await this._memberRepository.ManyByFilter(request.AsFilter(),request.Page, request.Limit, cancellationToken);
       return CollectionResult<MemberResult>.FromValue(
           members.Select(m => this._mapper.Map<MemberResult>(m)).ToList(), members.Count());
    }
}