using ONPA.Apply.Api.Application.Commands;
using PLUG.System.Apply.Domain;
using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;

namespace ONPA.Apply.Api.Application.CommandHandlers;

public sealed class CreateApplicationFormCommandHandler : ApplicationCommandHandlerBase<CreateApplicationFormCommand>
{
    private readonly IAggregateRepository<ApplicationForm> _aggregateRepository;

    public CreateApplicationFormCommandHandler(IAggregateRepository<ApplicationForm> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(CreateApplicationFormCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = new ApplicationForm(request.FirstName, request.LastName, request.Email, request.Phone,
                request.Recommendations,
                request.Address);
            await this._aggregateRepository.CreateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}