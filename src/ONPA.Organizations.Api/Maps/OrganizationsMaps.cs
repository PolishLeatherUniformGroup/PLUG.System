using AutoMapper;
using ONPA.Organizations.Api.Application.Commands;
using ONPA.Organizations.Api.Application.Queries;
using ONPA.Organizations.Contract.Requests;
using ONPA.Organizations.Contract.Responses;
using ONPA.Organizations.Infrastructure.ReadModel;
using PLUG.System.SharedDomain;
using OrganizationSettings = ONPA.Organizations.Infrastructure.ReadModel.OrganizationSettings;

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
            .ForCtorParam(nameof(CreateOrganizationCommand.Regon), opt => opt.MapFrom(src => src.Regon))
            .ForAllMembers(options => options.Ignore());

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
            .ForCtorParam(nameof(ChangeOrganizationDataCommand.Regon), opt => opt.MapFrom(src => src.Data.Regon))
            .ForCtorParam(nameof(ChangeOrganizationDataCommand.ContactEmail),
                opt => opt.MapFrom(src => src.Data.ContactEmail))
            .ForAllMembers(options => options.Ignore());

        this.CreateMap<UpdateOrganizationSettingsRequest, UpdateOrganizationSettingsCommand>()
            .ForCtorParam(nameof(UpdateOrganizationSettingsCommand.OrganizationId),
                opt => opt.MapFrom(src => src.OrganizationId))
            .ForCtorParam(nameof(UpdateOrganizationSettingsCommand.RequiredRecommendations),
                opt => opt.MapFrom(src => src.Settings.RequiredRecommendations))
            .ForCtorParam(nameof(UpdateOrganizationSettingsCommand.DaysForAppeal),
                opt => opt.MapFrom(src => src.Settings.DaysForAppeal))
            .ForCtorParam(nameof(UpdateOrganizationSettingsCommand.FeePaymentMonth),
                opt => opt.MapFrom(src => src.Settings.FeePaymentMonth))
            .ForAllMembers(options => options.Ignore());

        this.CreateMap<AddMembershipFeeRequest, RequestMembershipFeeCommand>()
            .ForCtorParam(nameof(RequestMembershipFeeCommand.OrganizationId),
                opt => opt.MapFrom(src => src.OrganizationId))
            .ForCtorParam(nameof(RequestMembershipFeeCommand.Year),
                opt => opt.MapFrom(src => src.Fee.Year))
            .ForCtorParam(nameof(RequestMembershipFeeCommand.Amount),
                opt => opt.MapFrom(src => new Money( src.Fee.Amount, src.Fee.Currency)))
            .ForAllMembers(options => options.Ignore());

        this.CreateMap<OrganizationFee, OrganizationFeeResponse>()
            .ForCtorParam(nameof(OrganizationFeeResponse.Amount), opt => opt.MapFrom(src => src.Amount))
            .ForCtorParam(nameof(OrganizationFeeResponse.Currency), opt => opt.MapFrom(src => src.Currency))
            .ForAllMembers(options => options.Ignore());

        this.CreateMap<OrganizationSettings, OrganizationSettingsResponse>()
            .ForCtorParam(nameof(OrganizationSettingsResponse.RequiredRecommendations),
                opt => opt.MapFrom(src => src.RequiredRecommendations))
            .ForCtorParam(nameof(OrganizationSettingsResponse.DaysForAppeal),
                opt => opt.MapFrom(src => src.DaysForAppeal))
            .ForCtorParam(nameof(OrganizationSettingsResponse.FeePaymentMonth),
                opt => opt.MapFrom(src => src.FeePaymentMonth))
            .ForAllMembers(options => options.Ignore());

        this.CreateMap<Organization, OrganizationResponse>()
            .ForCtorParam(nameof(OrganizationResponse.Id), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(OrganizationResponse.Name), opt => opt.MapFrom(src => src.Name))
            .ForCtorParam(nameof(OrganizationResponse.CardPrefix), opt => opt.MapFrom(src => src.CardPrefix))
            .ForCtorParam(nameof(OrganizationResponse.Address), opt => opt.MapFrom(src => src.Address))
            .ForCtorParam(nameof(OrganizationResponse.TaxId), opt => opt.MapFrom(src => src.TaxId))
            .ForCtorParam(nameof(OrganizationResponse.AccountNumber), opt => opt.MapFrom(src => src.AccountNumber))
            .ForCtorParam(nameof(OrganizationResponse.Regon), opt => opt.MapFrom(src => src.Regon))
            .ForCtorParam(nameof(OrganizationResponse.ContactEmail), opt => opt.MapFrom(src => src.ContactEmail))
            .ForAllMembers(options => options.Ignore());

        this.CreateMap<GetOrganizationRequest, GetOrganizationQuery>()
            .ForCtorParam(nameof(GetOrganizationQuery.OrganizationId), opt => opt.MapFrom(src => src.OrganizationId))
            .ForAllMembers(options => options.Ignore());

        this.CreateMap<GetOrganizationSettingsRequest, GetOrganizationSettingsQuery>()
            .ForCtorParam(nameof(GetOrganizationSettingsQuery.OrganizationId),
                opt => opt.MapFrom(src => src.OrganizationId))
            .ForAllMembers(options => options.Ignore());

        this.CreateMap<GetOrganizationsRequest, GetOrganizationsQuery>()
            .ForCtorParam(nameof(GetOrganizationsQuery.Page), opt => opt.MapFrom(src => src.Page))
            .ForCtorParam(nameof(GetOrganizationsQuery.Limit), opt => opt.MapFrom(src => src.Limit))
            .ForAllMembers(options => options.Ignore());

        this.CreateMap<GetOrganizationFeesRequest, GetOrganizationFeesQuery>()
            .ForCtorParam(nameof(GetOrganizationFeesQuery.OrganizationId),
                opt => opt.MapFrom(src => src.OrganizationId))
            .ForCtorParam(nameof(GetOrganizationFeesQuery.Page), opt => opt.MapFrom(src => src.Page))
            .ForCtorParam(nameof(GetOrganizationFeesQuery.Limit), opt => opt.MapFrom(src => src.Limit))
            .ForAllMembers(options => options.Ignore());

        this.CreateMap<GetOrganizationFeeForYearRequest, GetOrganizationFeeForYearQuery>()
            .ForCtorParam(nameof(GetOrganizationFeeForYearQuery.OrganizationId),
                opt => opt.MapFrom(src => src.OrganizationId))
            .ForCtorParam(nameof(GetOrganizationFeeForYearQuery.Year), opt => opt.MapFrom(src => src.Year))
            .ForAllMembers(options => options.Ignore());
            
        this.CreateMap<OrganizationFee, OrganizationFeeResponse>()
            .ForCtorParam(nameof(OrganizationFeeResponse.Amount), opt => opt.MapFrom(src => src.Amount))
            .ForCtorParam(nameof(OrganizationFeeResponse.Currency), opt => opt.MapFrom(src => src.Currency))
            .ForAllMembers(options => options.Ignore());
    }
}