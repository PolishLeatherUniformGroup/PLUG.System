using PLUG.System.Apply.Api.Application.Queries;
using PLUG.System.Apply.Api.Application.Queries.Results;
using PLUG.System.Apply.Infrastructure.ReadModel;
using PLUG.System.Common.Application;

namespace PLUG.System.Apply.Api.Application.QueryHandlers;

public sealed class GetApplicationsByStatusQueryHandler : CollectionQueryHandlerBase<GetApplicationsByStatusQuery, ApplicationResult>
{
    private readonly IReadOnlyRepository<ApplicationForm> _repository;

    public GetApplicationsByStatusQueryHandler(IReadOnlyRepository<ApplicationForm> repository)
    {
        this._repository = repository;
    }

    public override async Task<CollectionResult<ApplicationResult>> Handle(GetApplicationsByStatusQuery request, CancellationToken cancellationToken)
    {
        var applicationForms = await this._repository.ManyByFilter(x => x.Status == request.Status, request.Page, request.Limit, cancellationToken);
        var result = applicationForms.Select(x => new ApplicationResult(x.Id, x.FirstName, x.LastName, x.ApplicationDate));
        
        return CollectionResult<ApplicationResult>.FromValue(result, result.Count());
        
    }
}