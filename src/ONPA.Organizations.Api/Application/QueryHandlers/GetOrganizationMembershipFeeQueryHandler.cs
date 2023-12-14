using AutoMapper;
using ONPA.Common.Application;
using ONPA.Organizations.Api.Application.Queries;
using ONPA.Organizations.Contract.Responses;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Api.Application.QueryHandlers;

public sealed class GetOrganizationMembershipFeeQueryHandler : ApplicationQueryHandlerBase<GetOrganizationMembershipFeeQuery, OrganizationFeeResponse?>
{
    private readonly IReadOnlyRepository<OrganizationFee> _repository;
    private readonly IMapper _mapper;

    public GetOrganizationMembershipFeeQueryHandler(IReadOnlyRepository<OrganizationFee> repository, IMapper mapper)
    {
        this._repository = repository;
        this._mapper = mapper;
    }

    public override async Task<OrganizationFeeResponse?> Handle(GetOrganizationMembershipFeeQuery request, CancellationToken cancellationToken)
    {
        var organizationFee= (await this._repository.ManyByFilter(request.AsFilter() ,0,1, cancellationToken)).FirstOrDefault();
        if (organizationFee is null)
        {
            return null;
        }
        var result = this._mapper.Map<OrganizationFeeResponse>(organizationFee);
        return result;
    }
}