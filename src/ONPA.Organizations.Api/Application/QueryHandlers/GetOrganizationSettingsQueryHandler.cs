using AutoMapper;
using ONPA.Common.Application;
using ONPA.Organizations.Api.Application.Queries;
using ONPA.Organizations.Contract.Responses;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Api.Application.QueryHandlers;

public sealed class GetOrganizationSettingsQueryHandler : ApplicationQueryHandlerBase<GetOrganizationSettingsQuery, OrganizationSettingsResponse?>
{
    private readonly IReadOnlyRepository<OrganizationSettings> _repository;
    private readonly IMapper _mapper;

    public GetOrganizationSettingsQueryHandler(IReadOnlyRepository<OrganizationSettings> repository, IMapper mapper)
    {
        this._repository = repository;
        this._mapper = mapper;
    }

    public override async Task<OrganizationSettingsResponse?> Handle(GetOrganizationSettingsQuery request, CancellationToken cancellationToken)
    {
        var settings= await this._repository.ReadSingleById(request.OrganizationId, cancellationToken);
        if (settings is null)
        {
            return null;
        }
        var result = this._mapper.Map<OrganizationSettingsResponse>(settings);
        return result;
    }
}