using AutoMapper;
using ONPA.Common.Application;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Responses;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Api.Application.QueryHandlers;

public sealed class GetMemberByIdQueryHandler : ApplicationQueryHandlerBase<GetMemberByIdQuery, MemberResult>
{
    private readonly IReadOnlyRepository<Member> _memberRepository;
    private readonly IMapper _mapper;
    
    public GetMemberByIdQueryHandler(IReadOnlyRepository<Member> memberRepository, IMapper mapper)
    {
        this._memberRepository = memberRepository;
        this._mapper = mapper;
    }
    public override async Task<MemberResult> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await this._memberRepository.ReadSingleById(request.MemberId, cancellationToken);
        return this._mapper.Map<MemberResult>(member);
    }
}