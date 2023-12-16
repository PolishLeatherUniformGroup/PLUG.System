using AutoMapper;
using ONPA.Apply.Api.Application.Commands;
using ONPA.Apply.Api.Application.Queries;
using ONPA.Apply.Contract.Requests;
using PLUG.System.SharedDomain;

namespace ONPA.Apply.Api.Maps;

public class ApplyMaps : Profile
{
    public ApplyMaps()
    {
        this.CreateMap<CreateApplicationRequest, CreateApplicationFormCommand>()
            .ForCtorParam(nameof(CreateApplicationFormCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(CreateApplicationFormCommand.FirstName), o =>
                o.MapFrom(s => s.FirstName))
            .ForCtorParam(nameof(CreateApplicationFormCommand.LastName),
                o =>
                    o.MapFrom(s => s.LastName))
            .ForCtorParam(nameof(CreateApplicationFormCommand.Email), o =>
                o.MapFrom(s => s.Email))
            .ForCtorParam(nameof(CreateApplicationFormCommand.Address), o =>
                o.MapFrom(s => s.Address))
            .ForCtorParam(nameof(CreateApplicationFormCommand.Recommendations), o =>
                o.MapFrom(s => s.Recommenders));

        this.CreateMap<ApproveApplicationRequest, ApproveApplicationCommand>()
            .ForCtorParam(nameof(ApproveApplicationCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(ApproveApplicationCommand.ApplicationId), o =>
                o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(ApproveApplicationCommand.DecisionDate), o =>
                o.MapFrom(s => s.Decision.DecisionDate));

        this.CreateMap<RejectApplicationRequest, RejectApplicationCommand>()
            .ForCtorParam(nameof(RejectApplicationCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(RejectApplicationCommand.ApplicationId), o =>
                o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(RejectApplicationCommand.DaysToAppeal), o =>
                o.MapFrom(s => s.Decision.DaysToAppeal))
            .ForCtorParam(nameof(RejectApplicationCommand.DecisionDetail), o =>
                o.MapFrom(s => s.Decision.DecisionDetail))
            .ForCtorParam(nameof(RejectApplicationCommand.RejectionDate), o =>
                o.MapFrom(s => s.Decision.DecisionDate));

        this.CreateMap<EndorseRecommendationRequest, EndorseApplicationRecommendationCommand>()
            .ForCtorParam(nameof(EndorseApplicationRecommendationCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(EndorseApplicationRecommendationCommand.ApplicationFormId), o =>
                o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(EndorseApplicationRecommendationCommand.RecommendationId), o =>
                o.MapFrom(s => s.RecommendationId))
            .ForCtorParam(nameof(EndorseApplicationRecommendationCommand.RecommendingMemberId), o =>
                o.MapFrom(s => s.Endorsement.RecommendingMemberId));

        this.CreateMap<RefuseRecommendationRequest, RefuseApplicationRecommendationCommand>()
            .ForCtorParam(nameof(RefuseApplicationRecommendationCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(RefuseApplicationRecommendationCommand.ApplicationFormId), o =>
                o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(RefuseApplicationRecommendationCommand.RecommendationId), o =>
                o.MapFrom(s => s.RecommendationId))
            .ForCtorParam(nameof(RefuseApplicationRecommendationCommand.RecommendingMemberId), o =>
                o.MapFrom(s => s.Refusal.RecommendingMemberId));

        this.CreateMap<RegisterApplicationPaymentRequest, RegisterApplicationFeePaymentCommand>()
            .ForCtorParam(nameof(RegisterApplicationFeePaymentCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(RegisterApplicationFeePaymentCommand.ApplicationId),
                o =>
                    o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(RegisterApplicationFeePaymentCommand.Paid), o =>
                o.MapFrom(s => new Money(s.Payment.Amount, s.Payment.Currency)))
            .ForCtorParam(nameof(RegisterApplicationFeePaymentCommand.PaidDate),
                o => o.MapFrom(s => s.Payment.PaymentDate));

        this.CreateMap<AppealApplicationRejectionRequest, AppealApplicationRejectionCommand>()
            .ForCtorParam(nameof(AppealApplicationRejectionCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(AppealApplicationRejectionCommand.ApplicationId), o =>
                o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(AppealApplicationRejectionCommand.AppealReceived), o =>
                o.MapFrom(s => s.Appeal.ReceivedDate))
            .ForCtorParam(nameof(AppealApplicationRejectionCommand.Justification), o =>
                o.MapFrom(s => s.Appeal.Justification));

        this.CreateMap<GetApplicationsRequest, GetApplicationsByStatusQuery>()
            .ForCtorParam(nameof(GetApplicationsByStatusQuery.Status), o =>
                o.MapFrom(s => s.Status))
            .ForCtorParam(nameof(GetApplicationsByStatusQuery.Limit), o =>
                o.MapFrom(s => s.Limit))
            .ForCtorParam(nameof(GetApplicationsByStatusQuery.Page), o =>
                o.MapFrom(s => s.Page));

        this.CreateMap<GetApplicationRequest, GetApplicationQuery>()
            .ForCtorParam(nameof(GetApplicationQuery.ApplicationId), o =>
                o.MapFrom(s => s.ApplicationId));

        this.CreateMap<ApproveAppealApplicationRejectionRequest, ApproveApplicationRejectionAppealCommand>()
            .ForCtorParam(nameof(ApproveApplicationRejectionAppealCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(ApproveApplicationRejectionAppealCommand.ApplicationId), o =>
                o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(ApproveApplicationRejectionAppealCommand.AcceptDate), o =>
                o.MapFrom(s => s.Approval.ApproveDate))
            .ForCtorParam(nameof(ApproveApplicationRejectionAppealCommand.Justification), o =>
                o.MapFrom(s => s.Approval.Justification));

        this.CreateMap<RejectAppealApplicationRejectionRequest, DismissApplicationRejectionAppealCommand>()
            .ForCtorParam(nameof(DismissApplicationRejectionAppealCommand.TenantId), o =>
                o.MapFrom(s => s.TenantId))
            .ForCtorParam(nameof(DismissApplicationRejectionAppealCommand.ApplicationId), o =>
                o.MapFrom(s => s.ApplicationId))
            .ForCtorParam(nameof(DismissApplicationRejectionAppealCommand.RejectDate), o =>
                o.MapFrom(s => s.Rejection.RejectionDate))
            .ForCtorParam(nameof(DismissApplicationRejectionAppealCommand.DecisionDetail), o =>
                o.MapFrom(s => s.Rejection.Justification));
    }
}