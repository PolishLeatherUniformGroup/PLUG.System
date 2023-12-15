using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Organizations.Api.Application.Commands;
using ONPA.Organizations.Domain;

namespace ONPA.Organizations.Api.Application.CommandHandlers;

public sealed class ChangeOrganizationDataCommandHandler : ApplicationCommandHandlerBase<ChangeOrganizationDataCommand>
{
    private readonly IAggregateRepository<Organization> _aggregateRepository;

    public ChangeOrganizationDataCommandHandler(IAggregateRepository<Organization> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(ChangeOrganizationDataCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.OrganizationId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }
            aggregate.UpdateOrganizationData(request.Name, request.CardPrefix, request.TaxId, request.AccountNumber, request.Address, request.ContactEmail, request.Regon);
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}