using System.Linq.Expressions;
using PLUG.System.Apply.Api.Application.Queries.Results;
using PLUG.System.Apply.Domain;
using PLUG.System.Common.Application;

namespace PLUG.System.Apply.Api.Application.Queries;

public record GetApplicationsByStatusQuery : ApplicationCollectionQueryBase<ApplicationResult>
{
    public int Status { get; init; }
    
    public GetApplicationsByStatusQuery(int status, int page, int limit) : base(page, limit)
    {
        Status = status;
    }

}