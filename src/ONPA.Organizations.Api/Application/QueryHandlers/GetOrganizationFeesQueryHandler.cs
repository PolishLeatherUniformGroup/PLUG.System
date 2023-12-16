using AutoMapper;
using ONPA.Common.Application;
using ONPA.Organizations.Api.Application.Queries;
using ONPA.Organizations.Contract.Responses;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Api.Application.QueryHandlers;

public sealed class GetOrganizationFeesQueryHandler : CollectionQueryHandlerBase<GetOrganizationFeesQuery, OrganizationFeeResponse>
{
    private readonly IReadOnlyRepository<OrganizationFee> _repository;
    private readonly IMapper _mapper;

    public GetOrganizationFeesQueryHandler(IReadOnlyRepository<OrganizationFee> repository, IMapper mapper)
    {
        this._repository = repository;
        this._mapper = mapper;
    }

    public override async Task<CollectionResult<OrganizationFeeResponse>> Handle(GetOrganizationFeesQuery request, CancellationToken cancellationToken)
    {
        var fees = await this._repository.ReadMany(request.Page, request.Limit, cancellationToken);
        var result = CollectionResult<OrganizationFeeResponse>.FromValue(
            fees.Select(fee => this._mapper.Map<OrganizationFeeResponse>(fee)).ToList(),fees.Count());
        return result;
    }
}