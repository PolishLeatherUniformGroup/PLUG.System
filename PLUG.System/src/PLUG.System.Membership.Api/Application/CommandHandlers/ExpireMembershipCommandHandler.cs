using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.Api.Application.Commands;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.Api.Application.CommandHandlers;

public sealed class ExpireMembershipCommandHandler : ApplicationCommandHandlerBase<ExpireMembershipCommand>
{
    private readonly IAggregateRepository<Member> _aggregateRepository;

    public ExpireMembershipCommandHandler(IAggregateRepository<Member> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }
    
    public override async Task<CommandResult> Handle(ExpireMembershipCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.MemberId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }
            aggregate.MembershipExpired(request.ExpirationDate,request.Reason);
            aggregate = await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}