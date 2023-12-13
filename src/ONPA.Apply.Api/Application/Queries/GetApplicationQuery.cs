using ONPA.Apply.Contract.Responses;
using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Queries;

public record GetApplicationQuery : ApplicationQueryBase<ApplicationDetails?>
{
    public Guid ApplicationId { get; init; }

    public GetApplicationQuery(Guid applicationId)
    {
        this.ApplicationId = applicationId;
    }
}