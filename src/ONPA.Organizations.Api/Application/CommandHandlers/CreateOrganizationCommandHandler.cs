using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Organizations.Api.Application.Commands;
using ONPA.Organizations.Domain;

namespace ONPA.Organizations.Api.Application.CommandHandlers;

public sealed class CreateOrganizationCommandHandler : ApplicationCommandHandlerBase<CreateOrganizationCommand>
{
    private readonly IAggregateRepository<Organization> _aggregateRepository;

    public CreateOrganizationCommandHandler(IAggregateRepository<Organization> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = new Organization(request.Name, request.CardPrefix, request.TaxId, request.AccountNumber, request.Address, request.ContactEmail, request.Regon);
            await this._aggregateRepository.CreateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}