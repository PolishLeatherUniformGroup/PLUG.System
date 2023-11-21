using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.Api.Application.Commands;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.Api.Application.CommandHandlers;

public class CreateMemberCommandHandler : ApplicationCommandHandlerBase<CreateMemberCommand>
{
    private readonly IAggregateRepository<Member> _aggregateRepository;

    public CreateMemberCommandHandler(IAggregateRepository<Member> aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cardNumber = new CardNumber(1);
            var aggregate = new Member(
                cardNumber, request.FirstName, request.LastName, request.Email, request.Phone,
                request.Address, request.JoinDate, request.PaidFee);
            aggregate= await this._aggregateRepository.CreateAsync(aggregate, cancellationToken);
            return CommandResult.Success(aggregate.AggregateId);
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}