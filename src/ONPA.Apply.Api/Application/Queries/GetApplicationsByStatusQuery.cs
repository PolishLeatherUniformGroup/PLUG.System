using ONPA.Apply.Contract.Responses;
using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Queries;

public record GetApplicationsByStatusQuery : ApplicationCollectionQueryBase<ApplicationResult>
{
    public int Status { get; init; }
    
    public GetApplicationsByStatusQuery(Guid tenantId,int status, int page, int limit) : base(tenantId,page, limit)
    {
        this.Status = status;
    }

}