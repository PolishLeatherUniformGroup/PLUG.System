using ONPA.Common.Application;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Responses;
using ReadModel=ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Api.Application.QueryHandlers;

public sealed class ValidateMemberNumberQueryHandler: ApplicationQueryHandlerBase<ValidateMemberNumberQuery, MemberValidationResult>
{
    private readonly IReadOnlyRepository<ReadModel.Member> _memberRepository;

    public ValidateMemberNumberQueryHandler(IReadOnlyRepository<ReadModel.Member> memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public override async Task<MemberValidationResult> Handle(ValidateMemberNumberQuery request, CancellationToken cancellationToken)
    {
        var member = (await _memberRepository.ManyByFilter(x => x.MemberNumber == request.MemberNumber,0,1, cancellationToken)).SingleOrDefault();
        if(member is null)
        {
            return null;
        }
        return new MemberValidationResult(member.Id, member.MemberNumber);
        
    }
}