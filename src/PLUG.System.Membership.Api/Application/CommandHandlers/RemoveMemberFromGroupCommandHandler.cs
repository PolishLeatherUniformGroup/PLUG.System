using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.Api.Application.Commands;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.Api.Application.CommandHandlers;

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