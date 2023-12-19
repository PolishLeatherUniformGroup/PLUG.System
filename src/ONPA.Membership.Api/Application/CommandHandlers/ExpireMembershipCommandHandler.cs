using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Domain;

namespace ONPA.Membership.Api.Application.CommandHandlers;

public sealed class ExpireMembershipCommandHandler : MultiTenantApplicationCommandHandlerBase<ExpireMembershipCommand>
{
    private readonly IMultiTenantAggregateRepository<Member> _aggregateRepository;

    public ExpireMembershipCommandHandler(IMultiTenantAggregateRepository<Member> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }
    
    public override async Task<CommandResult> Handle(ExpireMembershipCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.TenantId, request.MemberId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }
            aggregate.MembershipExpired(request.ExpirationDate,request.Reason);
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}