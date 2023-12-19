using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Domain;

namespace ONPA.Membership.Api.Application.CommandHandlers;

public sealed class CreateMembersGroupCommandHandler: MultiTenantApplicationCommandHandlerBase<CreateMembersGroupCommand>
{
    private readonly IMultiTenantAggregateRepository<MembersGroup> _aggregateRepository;

    public CreateMembersGroupCommandHandler(IMultiTenantAggregateRepository<MembersGroup> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(CreateMembersGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = new MembersGroup(request.TenantId ,request.GroupName, request.GroupType);
            await this._aggregateRepository.CreateAsync(aggregate, cancellationToken);
            return CommandResult.Success(aggregate.AggregateId);
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}