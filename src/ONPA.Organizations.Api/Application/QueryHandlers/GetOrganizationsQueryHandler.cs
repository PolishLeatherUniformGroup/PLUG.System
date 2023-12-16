using AutoMapper;
using ONPA.Common.Application;
using ONPA.Organizations.Api.Application.Queries;
using ONPA.Organizations.Contract.Responses;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Api.Application.QueryHandlers;

public sealed class GetOrganizationsQueryHandler : CollectionQueryHandlerBase<GetOrganizationsQuery, OrganizationResponse>
{
    private readonly IReadOnlyRepository<Organization> _repository;
    private readonly IMapper _mapper;

    public GetOrganizationsQueryHandler(IReadOnlyRepository<Organization> repository, IMapper mapper)
    {
        this._repository = repository;
        this._mapper = mapper;
    }

    public override async Task<CollectionResult<OrganizationResponse>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var organizations = await this._repository.ReadMany(request.Page, request.Limit, cancellationToken);
        var result = CollectionResult<OrganizationResponse>.FromValue(
            organizations.Select(this._mapper.Map<OrganizationResponse>), organizations.Count());
        return result;
    }
}