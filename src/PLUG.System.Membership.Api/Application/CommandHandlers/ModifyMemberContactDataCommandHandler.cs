using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.Api.Application.Commands;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.Api.Application.CommandHandlers;

public sealed class ModifyMemberContactDataCommandHandler : ApplicationCommandHandlerBase<ModifyMemberContactDataCommand>
{
    private readonly IAggregateRepository<Member> _aggregateRepository;

    public ModifyMemberContactDataCommandHandler(IAggregateRepository<Member> aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }
    
    public override async Task<CommandResult> Handle(ModifyMemberContactDataCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.MemberId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }
            aggregate.ModifyContactData(request.Email,request.Phone,request.Address);
            aggregate = await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}