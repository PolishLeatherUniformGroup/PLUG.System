using AutoMapper;
using ONPA.Common.Application;
using ONPA.Organizations.Api.Application.Queries;
using ONPA.Organizations.Contract.Responses;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Api.Application.QueryHandlers;

public sealed class
    GetOrganizationBySlugQueryHandler : ApplicationQueryHandlerBase<GetOrganizationBySlugQuery, TenantInfo>
{
    private readonly IReadOnlyRepository<Organization> _repository;
    private readonly IMapper _mapper;

    public GetOrganizationBySlugQueryHandler(IReadOnlyRepository<Organization> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override async Task<TenantInfo> Handle(GetOrganizationBySlugQuery request, CancellationToken cancellationToken)
    {
        //var result = await _repository.ManyByFilter(x=>x.CardPrefix == request.Slug, 0, 1, cancellationToken);
        //var found =result.SingleOrDefault();
        var found = new Organization()
        {
            Id = Guid.NewGuid(),
            AccountNumber = "123456789",
            Address = "Address",
            CardPrefix = "DEMO",
            Name = "Organizacja Demo",
            Regon = "123456789",
            ContactEmail = "org@some.com",
            TaxId = "12"
        };
        return this._mapper.Map<TenantInfo>(found);
    }
}