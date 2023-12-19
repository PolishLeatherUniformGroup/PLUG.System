using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Domain;

namespace ONPA.Membership.Api.Application.CommandHandlers;

public class CreateMemberCommandHandler : MultiTenantApplicationCommandHandlerBase<CreateMemberCommand>
{
    private readonly IMultiTenantAggregateRepository<Member> _aggregateRepository;

    public CreateMemberCommandHandler(IMultiTenantAggregateRepository<Member> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cardNumber = new CardNumber(1);
            var aggregate = new Member(request.TenantId,
                cardNumber, request.FirstName, request.LastName, request.Email, request.Phone,
                request.Address, request.JoinDate, request.PaidFee);
            await this._aggregateRepository.CreateAsync(aggregate, cancellationToken);
            return CommandResult.Success(aggregate.AggregateId);
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}