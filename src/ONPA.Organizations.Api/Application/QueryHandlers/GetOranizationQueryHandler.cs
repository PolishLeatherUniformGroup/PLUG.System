using AutoMapper;
using ONPA.Common.Application;
using ONPA.Organizations.Api.Application.Queries;
using ONPA.Organizations.Contract.Responses;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Api.Application.QueryHandlers;

public sealed class GetOranizationQueryHandler : ApplicationQueryHandlerBase<GetOrganizationQuery, OrganizationResponse?>
{
    private readonly IReadOnlyRepository<Organization> _repository;
    private readonly IMapper _mapper;

    public GetOranizationQueryHandler(IReadOnlyRepository<Organization> repository, IMapper mapper)
    {
        this._repository = repository;
        this._mapper = mapper;
    }

    public override async Task<OrganizationResponse?> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
    {
        var organization = await this._repository.ReadSingleById(request.OrganizationId, cancellationToken);
        if (organization is null)
        {
            return null;
        }
        var result = this._mapper.Map<OrganizationResponse>(organization);
        return result;
    }
}