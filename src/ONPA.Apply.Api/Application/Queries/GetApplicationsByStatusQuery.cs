using System.Linq.Expressions;
using ONPA.Apply.Api.Application.Queries.Results;
using PLUG.System.Apply.Domain;
using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Queries;

public record GetApplicationsByStatusQuery : ApplicationCollectionQueryBase<ApplicationResult>
{
    public int Status { get; init; }
    
    public GetApplicationsByStatusQuery(int status, int page, int limit) : base(page, limit)
    {
        Status = status;
    }

}