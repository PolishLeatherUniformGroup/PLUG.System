using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.Api.Application.Commands;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.Api.Application.CommandHandlers;

public sealed class MakeMemberHonoraryCommandHandler : ApplicationCommandHandlerBase<MakeMemberHonoraryCommand>
{
    private readonly IAggregateRepository<Member> _aggregateRepository;

    public MakeMemberHonoraryCommandHandler(IAggregateRepository<Member> aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }
    
    public override async Task<CommandResult> Handle(MakeMemberHonoraryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.MemberId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }
            aggregate.MakeHonoraryMember();
            aggregate = await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}