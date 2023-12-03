using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.Api.Application.Commands;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.Api.Application.CommandHandlers;

public sealed class AddMemberToGroupCommandHandler : ApplicationCommandHandlerBase<AddMemberToGroupCommand>
{
    private readonly IAggregateRepository<MembersGroup> _aggregateRepository;

    public AddMemberToGroupCommandHandler(IAggregateRepository<MembersGroup> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(AddMemberToGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }
            aggregate.JoinGroup(request.MemberNumber, request.MemberId,request.FirstName,request.LastName,request.JoinDate);
            aggregate = await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return CommandResult.Success(aggregate.AggregateId);
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}