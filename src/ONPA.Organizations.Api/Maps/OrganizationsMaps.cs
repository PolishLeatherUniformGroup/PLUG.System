using AutoMapper;
using ONPA.Organizations.Api.Application.Commands;
using ONPA.Organizations.Contract.Requests;

namespace ONPA.Organizations.Api.Maps;

public class OrganizationsMaps : Profile
{
    public OrganizationsMaps()
    {
        this.CreateMap<CreateOrganizationRequest, CreateOrganizationCommand>()
            .ForCtorParam(nameof(CreateOrganizationCommand.Name), opt => opt.MapFrom(src => src.Name))
            .ForCtorParam(nameof(CreateOrganizationCommand.CardPrefix), opt => opt.MapFrom(src => src.CardPrefix))
            .ForCtorParam(nameof(CreateOrganizationCommand.Address), opt => opt.MapFrom(src => src.Address))
            .ForCtorParam(nameof(CreateOrganizationCommand.TaxId), opt => opt.MapFrom(src => src.TaxId))
            .ForCtorParam(nameof(CreateOrganizationCommand.AccountNumber), opt => opt.MapFrom(src => src.AccountNumber))
            .ForCtorParam(nameof(CreateOrganizationCommand.Regon), opt => opt.MapFrom(src => src.Regon));
    }
}