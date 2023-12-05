using System.Linq.Expressions;
using PLUG.System.Apply.Api.Application.Queries.Results;
using PLUG.System.Apply.Domain;
using PLUG.System.Common.Application;

namespace PLUG.System.Apply.Api.Application.Queries;

public record GetApplicationsByStatusQuery : ApplicationQueryBase<CollectionResult<ApplicationResult>>
{
    public int Status { get; init; }
    public int Page { get; init; }
    public int Limit { get; init; }
    
    public GetApplicationsByStatusQuery(int status, int page, int limit)
    {
        Status = status;
        Page = page;
        Limit = limit;
    }

}