using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Organizations.Api.Application.Commands;
using ONPA.Organizations.Domain;

namespace ONPA.Organizations.Api.Application.CommandHandlers;

public sealed class RequestMembershipFeeCommandHandler : ApplicationCommandHandlerBase<RequestMembershipFeeCommand>
{
    private readonly IAggregateRepository<Organization> _aggregateRepository;

    public RequestMembershipFeeCommandHandler(IAggregateRepository<Organization> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(RequestMembershipFeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.OrganizationId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }
            aggregate.RequestMembershipFee(request.Year, request.Amount);
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}