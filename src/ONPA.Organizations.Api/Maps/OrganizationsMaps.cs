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

        this.CreateMap<UpdateOrganizationDataRequest, ChangeOrganizationDataCommand>()
            .ForCtorParam(nameof(ChangeOrganizationDataCommand.OrganizationId),
                opt => opt.MapFrom(src => src.OrganizationId))
            .ForCtorParam(nameof(ChangeOrganizationDataCommand.Name), opt => opt.MapFrom(src => src.Data.Name))
            .ForCtorParam(nameof(ChangeOrganizationDataCommand.CardPrefix),
                opt => opt.MapFrom(src => src.Data.CardPrefix))
            .ForCtorParam(nameof(ChangeOrganizationDataCommand.Address), opt => opt.MapFrom(src => src.Data.Address))
            .ForCtorParam(nameof(ChangeOrganizationDataCommand.TaxId), opt => opt.MapFrom(src => src.Data.TaxId))
            .ForCtorParam(nameof(ChangeOrganizationDataCommand.AccountNumber),
                opt => opt.MapFrom(src => src.Data.AccountNumber))
            .ForCtorParam(nameof(ChangeOrganizationDataCommand.Regon), opt => opt.MapFrom(src => src.Data.Regon));

        this.CreateMap<UpdateOrganizationSettingsRequest, UpdateOrganizationSettingsCommand>()
            .ForCtorParam(nameof(UpdateOrganizationSettingsCommand.OrganizationId),
                opt => opt.MapFrom(src => src.OrganizationId))
            .ForCtorParam(nameof(UpdateOrganizationSettingsCommand.RequiredRecommendations),
                opt => opt.MapFrom(src => src.Settings.RequiredRecommendations))
            .ForCtorParam(nameof(UpdateOrganizationSettingsCommand.DaysForAppeal),
                opt => opt.MapFrom(src => src.Settings.DaysForAppeal))
            .ForCtorParam(nameof(UpdateOrganizationSettingsCommand.FeePaymentMonth),
                opt => opt.MapFrom(src => src.Settings.FeePaymentMonth));

        this.CreateMap<AddMembershipFeeRequest, RequestMembershipFeeCommand>()
            .ForCtorParam(nameof(RequestMembershipFeeCommand.OrganizationId),
                opt => opt.MapFrom(src => src.OrganizationId))
            .ForCtorParam(nameof(RequestMembershipFeeCommand.Year),
                opt => opt.MapFrom(src => src.Fee.Year))
            .ForCtorParam(nameof(RequestMembershipFeeCommand.Amount),
                opt => opt.MapFrom(src => src.Fee.Amount));
    }
}