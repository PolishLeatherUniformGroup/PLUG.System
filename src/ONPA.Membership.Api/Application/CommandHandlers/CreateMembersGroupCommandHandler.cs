using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Domain;

namespace ONPA.Membership.Api.Application.CommandHandlers;

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