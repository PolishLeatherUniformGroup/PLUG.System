using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Domain;

namespace ONPA.Membership.Api.Application.CommandHandlers;

public sealed class RemoveMemberFromGroupCommandHandler : ApplicationCommandHandlerBase<RemoveMemberFromGroupCommand>
{
    private readonly IAggregateRepository<MembersGroup> _aggregateRepository;

    public RemoveMemberFromGroupCommandHandler(IAggregateRepository<MembersGroup> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(RemoveMemberFromGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }
            aggregate.RemoveFromGroup(request.MemberNumber,request.RemoveDate);
            aggregate = await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return CommandResult.Success(aggregate.AggregateId);
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}