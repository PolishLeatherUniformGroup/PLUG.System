using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.Api.Application.Commands;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.Api.Application.CommandHandlers;

public sealed class CreateMembersGroupCommandHandler: ApplicationCommandHandlerBase<CreateMembersGroupCommand>
{
    private readonly IAggregateRepository<MembersGroup> _aggregateRepository;

    public CreateMembersGroupCommandHandler(IAggregateRepository<MembersGroup> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(CreateMembersGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = new MembersGroup(request.GroupName, request.GroupType);
            aggregate= await this._aggregateRepository.CreateAsync(aggregate, cancellationToken);
            return CommandResult.Success(aggregate.AggregateId);
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}